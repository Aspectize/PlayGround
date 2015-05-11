
var app = newApplication();

app.Directories = "Bootstrap";
app.URLRewritingService = "URLRewritingService";

app.AddAuthorizationRole(aas.Roles.Anonymous, aas.Enum.AccessControl.ReadWrite);


var ctxData = newContextData();

ctxData.Name = "MainData";
ctxData.NameSpaceList = "PlayGround";

var ctxData1 = newContextData();

ctxData1.Name = "CurrentSession";
ctxData1.IsDataSet = false;
