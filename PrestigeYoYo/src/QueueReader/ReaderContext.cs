using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Messaging;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Configuration;
using Prestige.Services;
using Prestige.Repositories;
using Prestige.DB;
using Prestige.DB.Models;
using System.Data.Entity;

namespace QueueReader
{
    public class ReaderContext : ApplicationContext
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ReaderContext" /> class.
        /// </summary>
        public ReaderContext()
        {
#if DEBUG
            Database.SetInitializer(new DebugInitialization());
#else
            Database.SetInitializer(new ReleaseInitialization());
#endif

            var dbContext = new PrestigeContext("Prestige");
            this.StageService = new ProductionStageService(
                            new ProductionStageRepository(dbContext));
            this.FlawService = new ProductFlawService(
                            new ProductFlawRepository(dbContext));
            this.StationService = new ProductionStationService(
                            new ProductionStationRepository(dbContext));

            this.Queue = new MessageQueue(
                        ConfigurationManager.AppSettings["QueueName"]);
            this.Queue.Formatter = new ActiveXMessageFormatter();
            this.Queue.MessageReadPropertyFilter.LookupId = true;
            this.Queue.ReceiveCompleted +=
                    new ReceiveCompletedEventHandler(msmq_ReceiveCompleted);
            this.Queue.BeginReceive();
        }

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
        /// Gets the queue.
        /// </summary>
        /// <value>
        /// The queue.
        /// </value>
        public MessageQueue Queue { get; private set; }

        /// <summary>
        /// Handles the ReceiveCompleted event of the msmq control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///   The <see cref="ReceiveCompletedEventArgs" />
        ///   instance containing the event data.
        /// </param>
        void msmq_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var str = e.Message.Body.ToString();
            Debug.WriteLine(str);
            var split = str.Split(",".ToCharArray());
            if (split[4] != "")
            {
                Debug.WriteLine("{0} - {1}", split[3], split[4]);
            }

            // WorkArea,
            // 21f27817-f307-49f9-a826-5f8d772cdfdf,
            // Line0,
            // INSPECTION_1_SCRAP,
            // INCONSISTENT_THICKNESS,
            // 11/12/2012 1:17:35 PM

            var stage = new ProductionStage();
            stage.WorkArea = split[0];
            stage.TimeStamp = DateTime.Parse(split[5]);

            var line = split[2].Substring(4);
            stage.LineNumber = int.Parse(line);

            stage.ProductionId = Guid.Parse(split[1]);

            var stationId = split[3];
            var station = this.StationService.List().FirstOrDefault(s => s.Identifier == stationId);

            if (station == null)
            {
                if (split[3].Contains("SCRAP"))
                {
                    station = this.StationService.List().FirstOrDefault(s => s.Identifier == "SCRAP");
                }
            }

            stage.Station = station;

            if (split[4].Length > 0)
            {
                var flawId = split[4];
                stage.ProductFlaw = this.FlawService.List().FirstOrDefault(f => f.Identifier == flawId);
            }

            this.StageService.Add(stage);
            this.Queue.BeginReceive();
        }
    }
}
