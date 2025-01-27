using System;
using OrchardCore.AuditTrail.Indexes;
using OrchardCore.AuditTrail.Models;
using OrchardCore.Data.Migration;
using YesSql.Sql;

namespace OrchardCore.AuditTrail
{
    public class Migrations : DataMigration
    {
        public int Create()
        {
            SchemaBuilder.CreateMapIndexTable<AuditTrailEventIndex>(table => table
                .Column<string>(nameof(AuditTrailEventIndex.EventId), column => column.WithLength(26))
                .Column<string>(nameof(AuditTrailEventIndex.Category), column => column.WithLength(64))
                .Column<string>(nameof(AuditTrailEventIndex.Name), column => column.WithLength(64))
                .Column<string>(nameof(AuditTrailEventIndex.CorrelationId), column => column.WithLength(26))
                .Column<string>(nameof(AuditTrailEventIndex.UserId), column => column.WithLength(26))
                .Column<string>(nameof(AuditTrailEventIndex.NormalizedUserName), column => column.Nullable().WithLength(255))
                .Column<DateTime>(nameof(AuditTrailEventIndex.CreatedUtc), column => column.Nullable()),
                collection: AuditTrailEvent.Collection);

            SchemaBuilder.AlterIndexTable<AuditTrailEventIndex>(table => table
                .CreateIndex("IDX_AuditTrailEventIndex_DocumentId",
                    "DocumentId",
                    "EventId",
                    "Category",
                    "Name",
                    "CorrelationId",
                    "UserId",
                    "NormalizedUserName",
                    "CreatedUtc"
                    ),
                collection: AuditTrailEvent.Collection
            );

            return 1;
        }
    }
}
