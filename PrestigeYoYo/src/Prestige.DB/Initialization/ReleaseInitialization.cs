﻿/// ReleaseInitialization.cs
/// Thomas Kempton 2012
///

namespace Prestige.DB
{
    using System.Data.Entity;

    /// <summary>
    /// Release Database Initialization for Prestige.
    /// </summary>
    class ReleaseInitialization : CreateDatabaseIfNotExists<PrestigeContext>
    {
    }
}