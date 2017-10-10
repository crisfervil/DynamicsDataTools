﻿using log4net;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using XrmCommandBox.Data;

namespace XrmCommandBox.Tools
{
    public class ImportTool
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(ImportTool));
        private readonly IOrganizationService _crmService;

        public ImportTool(IOrganizationService service)
        {
            _crmService = service;
        }

        public void Run(ImportToolOptions options)
        {
            _log.Info("Running Import Tool...");

            var serializer = new DataTableSerializer();

            _log.Info("Reading file...");
            var dataTable = serializer.Deserialize(options.File);

            _log.Info("Processing records...");
            foreach (var row in dataTable)
            {
                var entity = GetEntity(row, dataTable.Name);
                _crmService.Create(entity);
            }

            _log.Info("Done!");
        }

        private Entity GetEntity(Dictionary<string,object> record, string entityName)
        {
            var entity = new Entity(entityName);

            foreach (var attrName in record.Keys)
            {
                // Convert this to the type specified in the metadata for the attribute in the entity
                entity[attrName] = GetValue(record[attrName]);
            }

            return entity;
        }

        private object GetValue(object value)
        {
            var convertedValue = value;
            var referenceValue = value as EntityReferenceValue;
            if (referenceValue != null)
            {
                convertedValue = new EntityReference(referenceValue.LogicalName,Guid.Parse((string)referenceValue.Value));
            }

            return convertedValue;
        }
    }
}
