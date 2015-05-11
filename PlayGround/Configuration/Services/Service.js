var dbSvc = Aspectize.ConfigureNewService("MyDataService", aas.ConfigurableServices.DataBaseService);

dbSvc.ConnectionString = "";
dbSvc.DataBaseType = aas.ConfigurableServices.DataBaseService.DBMS.AzureStorage;
dbSvc.BuildNewTableOnSave = true;

var fileSvc = Aspectize.ConfigureNewService("MyFileService", aas.ConfigurableServices.FileService);

fileSvc.AccessMode = aas.ConfigurableServices.FileService.FileAccessMode.Private;
fileSvc.ConnectionString = "";
fileSvc.RootDirectory = "~/RessourcesADW";
fileSvc.StorageType = aas.ConfigurableServices.FileService.Storage.Azure;




