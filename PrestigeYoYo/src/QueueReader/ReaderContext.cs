///
///
///

namespace QueueReader
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Configuration;
    using System.Data.Entity;
    using System.Drawing;
    using System.Linq;
    using System.Messaging;
    using System.Threading;
    using System.Windows.Forms;

    using Prestige.DB;
    using Prestige.DB.Models;
    using Prestige.Repositories;
    using Prestige.Services;

    public class ReaderContext : ApplicationContext
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ReaderContext" /> class.
        /// </summary>
        public ReaderContext()
        {
            this.InitializeServices();
            this.InitializeIcon();

            // connect to the message queue...
            this.Queue = new MessageQueue(
                        ConfigurationManager.AppSettings["QueueName"]);
            this.Queue.Formatter = new ActiveXMessageFormatter();
            this.Queue.MessageReadPropertyFilter.LookupId = true;
            this.Queue.ReceiveCompleted += msmq_ReceiveCompleted;

            // start reading
            this.IsRunning = true;
            this.Queue.BeginReceive();
        }

        /// <summary>
        /// Initializes the services.
        /// </summary>
        public void InitializeServices()
        {
            // set the database initializer
#if DEBUG
            Database.SetInitializer(new DebugInitialization());
#else
            Database.SetInitializer(new ReleaseInitialization());
#endif

            // set db context, repos, and services
            var dbContext = new PrestigeContext("Prestige");
            this.ProductionEntryService = new ProductionEntryService(
                            new ProductionEntryRepository(dbContext));
            this.ScheduleService = new ScheduleService(
                            new ScheduleRepository(dbContext));
            this.StageService = new ProductionStageService(
                            new ProductionStageRepository(dbContext));
            this.FlawService = new ProductFlawService(
                            new ProductFlawRepository(dbContext));
            this.StationService = new ProductionStationService(
                            new ProductionStationRepository(dbContext));
        }

        /// <summary>
        /// Initializes the icon.
        /// </summary>
        public void InitializeIcon()
        {
            // initialize the icon & menu
            var container = new Container();
            this.Icon = new NotifyIcon(container);
            this.MenuStart = new MenuItem("Start", Start_Click);
            this.MenuStart.Enabled = false;
            this.MenuStop = new MenuItem("Stop", Stop_Click);
            MenuItem close = new MenuItem("Close", Close_Click);
            this.Icon.ContextMenu = new ContextMenu();
            this.Icon.ContextMenu.MenuItems.Add(this.MenuStart);
            this.Icon.ContextMenu.MenuItems.Add(this.MenuStop);
            this.Icon.ContextMenu.MenuItems.Add("-");
            this.Icon.ContextMenu.MenuItems.Add(close);
            this.Icon.Icon = new Icon("yoyo.ico");
            this.Icon.Text = this.DisplayText + " - Running...";
            this.Icon.Visible = true;
        }

        /// <summary>
        /// Gets or sets the production entry service.
        /// </summary>
        /// <value>
        /// The production entry service.
        /// </value>
        private IProductionEntryService ProductionEntryService { get; set; }

        /// <summary>
        /// Gets or sets the schedule service.
        /// </summary>
        /// <value>
        /// The schedule service.
        /// </value>
        private IScheduleService ScheduleService { get; set; }
        
        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        /// <value>
        /// The service.
        /// </value>
        private IProductionStageService StageService { get; set; }

        /// <summary>
        /// Gets or sets the flaw service.
        /// </summary>
        /// <value>
        /// The flaw service.
        /// </value>
        private IProductFlawService FlawService { get; set; }

        /// <summary>
        /// Gets or sets the station service.
        /// </summary>
        /// <value>
        /// The station service.
        /// </value>
        private IProductionStationService StationService { get; set; }

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        public NotifyIcon Icon { get; private set; }

        /// <summary>
        /// THe display text.
        /// </summary>
        private string DisplayText = "Prestige Queue Reader";

        /// <summary>
        /// Gets or sets the menu start.
        /// </summary>
        /// <value>
        /// The menu start.
        /// </value>
        public MenuItem MenuStart { get; set; }

        /// <summary>
        /// Gets or sets the menu stop.
        /// </summary>
        /// <value>
        /// The menu stop.
        /// </value>
        public MenuItem MenuStop { get; set; }

        /// <summary>
        /// Gets the queue.
        /// </summary>
        /// <value>
        /// The queue.
        /// </value>
        public MessageQueue Queue { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is running.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is running; otherwise, <c>false</c>.
        /// </value>
        private bool IsRunning { get; set; }

        /// <summary>
        /// Handles the Click event of the Start control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///   The <see cref="EventArgs" /> instance containing the event data.
        /// </param>
        private void Start_Click(object sender, EventArgs e)
        {
            // set menu stuff, display name, start running...
            this.MenuStart.Enabled = false;
            this.MenuStop.Enabled = true;
            this.Icon.Text = this.DisplayText + " - Running...";
            this.IsRunning = true;
            this.Queue.BeginReceive();
        }

        /// <summary>
        /// Handles the Click event of the Stop control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///   The <see cref="EventArgs" /> instance containing the event data.
        /// </param>
        private void Stop_Click(object sender, EventArgs e)
        {
            // stop the presses!
            this.MenuStart.Enabled = true;
            this.MenuStop.Enabled = false;
            this.Icon.Text = this.DisplayText + " - Stopped";
            this.IsRunning = false;
        }

        /// <summary>
        /// Handles the Click event of the Close control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///   The <see cref="EventArgs" /> instance containing the event data.
        /// </param>
        private void Close_Click(object sender, EventArgs e)
        {
            // wait a bit, to let any running queue reading finish...
            if (this.IsRunning)
            {
                this.IsRunning = false;
                Thread.Sleep(500);
            }

            // exit!
            this.ExitThread();
        }

        /// <summary>
        /// Handles the ReceiveCompleted event of the msmq control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///   The <see cref="ReceiveCompletedEventArgs" />
        ///   instance containing the event data.
        /// </param>
        private void msmq_ReceiveCompleted(
                        object sender,
                        ReceiveCompletedEventArgs e)
        {
            var str = e.Message.Body.ToString();
            var split = str.Split(",".ToCharArray());

            // parse out message pieces
            Guid id = Guid.Parse(split[1]);
            var timestamp = DateTime.Parse(split[5]);
            var line = split[2].Substring(4);
            var lineNumber = int.Parse(line);

            // find an existing production entry if it exists
            var entry = this.ProductionEntryService.List().FirstOrDefault(
                            p => p.Id == id);

            // create a new entry if it doesnt exist yet...
            if (entry == null)
            {
                var schedule = this.ScheduleService.GetByTimestamp(timestamp);
                entry = new ProductionEntry()
                {
                    Id = id,
                    Product = schedule == null ? null : schedule.Product,
                    Stages = new Collection<ProductionStage>()
                };

                this.ProductionEntryService.Add(entry);
            }

            // create a new stage
            var stage = new ProductionStage()
            {
                WorkArea = split[0],
                TimeStamp = timestamp,
                LineNumber = lineNumber,
                ProductionEntry = entry
            };

            // find the station the product is at
            var stationId = split[3];
            var station = this.StationService.List().FirstOrDefault(
                                            s => s.Identifier == stationId);
            stage.Station = station;

            // find the flaw
            if (split[4].Length > 0)
            {
                var flawId = split[4];
                stage.ProductFlaw = this.FlawService.List().FirstOrDefault(
                                            f => f.Identifier == flawId);
            }

            // save the data!
            entry.Stages.Add(stage);
            this.ProductionEntryService.Update(entry);
            this.StageService.Add(stage);

            // start listening for messages again, if we aren't paused...
            if (this.IsRunning)
            {
                this.Queue.BeginReceive();
            }
        }
    }
}
