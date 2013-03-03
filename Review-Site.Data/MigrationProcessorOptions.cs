using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace Review_Site.Data
{
    /// <summary>
    /// A replacement for the aparrently missing class that is usually present in FluentMigrator.
    /// </summary>
    class MigrationProcessorOptions : IMigrationProcessorOptions
    {
        public bool PreviewOnly { get; set; }
        public int Timeout { get; set; }
    }
}
