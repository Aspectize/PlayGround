var dbSvc = Aspectize.ConfigureNewService("MyDataService", aas.ConfigurableServices.DataBaseService);

dbSvc.ConnectionString = "AccountName=aspectizeplayground;AccountKey=Afs+8MQMjf0rDsUaoznphvS+O+Ya6scd7UslS8EeV8bI4el47VBgsLosfDMLgaCvenSe1bAH9o3S9k33F9jp0g==;";
dbSvc.DataBaseType = aas.ConfigurableServices.DataBaseService.DBMS.AzureStorage;
dbSvc.BuildNewTableOnSave = true;

var ADWDB = Aspectize.ConfigureNewService("ADWDB", aas.ConfigurableServices.DataBaseService);

ADWDB.DataBaseType = aas.ConfigurableServices.DataBaseService.DBMS.AzureStorage;
ADWDB.ConnectionString = "AccountName=adventureworks;AccountKey=bpF8EHuzmIl+WEdFox77OIhC+QrEDhFSnHt2CHSyXHYgk5aTkgpkkzv9Q2EaBR1JNXlfHuae4HeUOcD1V4yJBw==;DefaultEndpointsProtocol=https;StorageStrategy=NewSort;";
ADWDB.Trace = false;
ADWDB.EnsureAuthenticationOnWrite = true;
ADWDB.BuildNewTableOnSave = false;

var mailService = Aspectize.ConfigureNewService("MailService", aas.ConfigurableServices.AspectizeSMTPService);

mailService.Expediteur = "playgroundrt@aspectize.com";
mailService.ExpediteurDisplay = "PlayGroundRT";
mailService.Host = "smtp.sendgrid.net";
mailService.Login = "azure_6f5691b5b5ebac6829cd5eb0f2a18660@azure.com";
mailService.Password = "ambg9jf3";
mailService.Port = "587";
mailService.Ssl = true;


var dbLogException = Aspectize.ConfigureNewService("MyDBlogException", aas.ConfigurableServices.DBLogException);

dbLogException.DataServiceName = "MyDataService";
dbLogException.MailServiceName = "MailService";
dbLogException.MailTo = "nicolas.roux@aspectize.com, frederic.fadel@aspectize.com";


