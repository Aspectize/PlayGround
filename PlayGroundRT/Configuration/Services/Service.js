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

var mailService = Aspectize.ConfigureNewService("MailService", aas.ConfigurableServices.AspectizeSMTPService);

mailService.Expediteur = "";
mailService.ExpediteurDisplay = "";
mailService.Host = "";
mailService.Login = "";
mailService.Password = "";
mailService.Port = "";
mailService.Ssl = true;


var dbLogException = Aspectize.ConfigureNewService("MyDBlogException", aas.ConfigurableServices.DBLogException);

dbLogException.DataServiceName = "MyDataService";
dbLogException.MailServiceName = "MailService";
dbLogException.MailTo = "";


