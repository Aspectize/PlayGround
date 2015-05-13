var dbSvc = Aspectize.ConfigureNewService("MyDataService", aas.ConfigurableServices.DataBaseService);

dbSvc.ConnectionString = "";
dbSvc.DataBaseType = aas.ConfigurableServices.DataBaseService.DBMS.AzureStorage;
dbSvc.BuildNewTableOnSave = true;

var fileSvc = Aspectize.ConfigureNewService("MyFileService", aas.ConfigurableServices.FileService);

fileSvc.AccessMode = aas.ConfigurableServices.FileService.FileAccessMode.Private;
fileSvc.ConnectionString = "";
fileSvc.RootDirectory = "~/RessourcesADW";
fileSvc.StorageType = aas.ConfigurableServices.FileService.Storage.Azure;

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



