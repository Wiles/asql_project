/// ReleaseInitialization.cs
/// Thomas Kempton 2012
///

namespace Prestige.DB
{
    using System.Data.Entity;

    class ReleaseInitialization : CreateDatabaseIfNotExists<PrestigeContext>
    {
    }
}
