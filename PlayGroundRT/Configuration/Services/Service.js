var dbSvc = Aspectize.ConfigureNewService("MyDataService", aas.ConfigurableServices.DataBaseService);

dbSvc.ConnectionString = "";
dbSvc.DataBaseType = aas.ConfigurableServices.DataBaseService.DBMS.AzureStorage;
dbSvc.BuildNewTableOnSave = true;

var ADWDB = Aspectize.ConfigureNewService("ADWDB", aas.ConfigurableServices.DataBaseService);

ADWDB.DataBaseType = aas.ConfigurableServices.DataBaseService.DBMS.AzureStorage;
ADWDB.ConnectionString = "";
ADWDB.Trace = false;
ADWDB.EnsureAuthenticationOnWrite = true;
ADWDB.BuildNewTableOnSave = false;
