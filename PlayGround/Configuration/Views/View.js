
var rootView = Aspectize.CreateView("RootView", aas.Controls.RootControl);
rootView.DisplayHTML.click.BindCommand(aas.Services.Browser.ClientService.TogglePanelAndResize(aas.ViewName.RootView.DisplayHTML, 'LeftPanel'));
rootView.DisplayCSS.click.BindCommand(aas.Services.Browser.ClientService.TogglePanelAndResize(aas.ViewName.RootView.DisplayCSS, 'MiddlePanel'));
rootView.DisplayJS.click.BindCommand(aas.Services.Browser.ClientService.TogglePanelAndResize(aas.ViewName.RootView.DisplayJS, 'RightPanel'));
rootView.DisplayBindings.click.BindCommand(aas.Services.Browser.ClientService.TogglePanel(aas.ViewName.RootView.DisplayBindings, 'CenterPanel'));

rootView.Run.click.BindCommand(aas.Services.Browser.ClientService.Run(aas.Data.MainData, aas.Data.MainData.Session.Id, aas.ViewName.IFrameResult.IFrameApplication, aas.Data.MainData.Session.Persist));
rootView.Save.click.BindCommand(aas.Services.Browser.ClientService.Run(aas.Data.MainData, aas.Data.MainData.Session.Id, aas.ViewName.IFrameResult.IFrameApplication, 'force'));

var mainView = Aspectize.CreateView("MainView", aas.Controls.SideBarContent, aas.Zones.RootView.ZoneMainContent);

mainView.UserControl.click.BindCommand(aas.Services.Browser.AceClientService.InsertCode(aas.ViewName.MainView.HTMLEditor, "\n<div aas:control='Test'>\n\n</div>"));
mainView.TextBox.click.BindCommand(aas.Services.Browser.AceClientService.InsertCode(aas.ViewName.MainView.HTMLEditor, "<input name='MyText' type='text' />"));
mainView.TextArea.click.BindCommand(aas.Services.Browser.AceClientService.InsertCode(aas.ViewName.MainView.HTMLEditor, "<textarea name='MyTextArea' cols='20' rows='2'></textarea>"));
mainView.Button.click.BindCommand(aas.Services.Browser.AceClientService.InsertCode(aas.ViewName.MainView.HTMLEditor, "<input name='MyButton' type='button' value='MyButton' />"));
mainView.Checkbox.click.BindCommand(aas.Services.Browser.AceClientService.InsertCode(aas.ViewName.MainView.HTMLEditor, "<input name='MyCheckBox' type='checkbox' />"));
mainView.ComboBox.click.BindCommand(aas.Services.Browser.AceClientService.InsertCode(aas.ViewName.MainView.HTMLEditor, "<select name='MySelect'></select>"));

mainView.OnLoad.BindCommand(aas.Services.Server.DataService.LoadSamples(), aas.Data.MainData, true);
mainView.SelectSamples.BindList(aas.Data.MainData.EnumInBaseSample, "Id", "Name", "Name");
mainView.SelectSamples.NullValueDisplay.BindData("Choose sample");
mainView.SelectSamples.SelectedValueChanged.BindCommand(aas.Services.Browser.ClientService.GetSession(mainView.SelectSamples.CurrentValue));
mainView.SelectSamples.SelectedValueChanged.BindCommand(aas.Services.Browser.ClientService.UpdateUrl(mainView.SelectSamples.CurrentValue));
mainView.SelectSamples.DefaultIndex.BindData(0);

mainView.CSSEditor.Mode.BindData('css');
mainView.CSSEditor.Value.BindData(aas.Data.MainData.Session.css);

mainView.HTMLEditor.Mode.BindData('html');
mainView.HTMLEditor.Value.BindData(aas.Data.MainData.Session.Html);

mainView.BindingEditor.Value.BindData(aas.Data.MainData.Session.Bindings);

mainView.JSEditor.Value.BindData(aas.Data.MainData.Session.js);

var errorResult = Aspectize.CreateView("ErrorResult", aas.Controls.ErrorControl, aas.Zones.MainView.ZoneResult);
errorResult.ErrorMessage.BindData(aas.Data.MainData.Session.Log, "{:S}");

var iFrameView = Aspectize.CreateView("IFrameResult", aas.Controls.IFrameControl, aas.Zones.MainView.ZoneResult);
