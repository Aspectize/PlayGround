
var rootView = Aspectize.CreateView("RootView", aas.Controls.RootControl);
rootView.DisplayHTML.click.BindCommand(aas.Services.Browser.ClientService.TogglePanelAndResize('DisplayHTML', 'HtmlPanel'));
rootView.DisplayCSS.click.BindCommand(aas.Services.Browser.ClientService.TogglePanelAndResize('DisplayCSS', 'CssPanel'));
rootView.DisplayJS.click.BindCommand(aas.Services.Browser.ClientService.TogglePanelAndResize('DisplayJS', 'JsPanel'));
rootView.DisplayBindings.click.BindCommand(aas.Services.Browser.ClientService.TogglePanel('DisplayBindings'));

rootView.DisplayHTML.BindData(aas.Expression(IIF(aas.Data.MainData.Session.DisplayHTML, 'active', '')));
rootView.DisplayCSS.BindData(aas.Expression(IIF(aas.Data.MainData.Session.DisplayCSS, 'active', '')));
rootView.DisplayJS.BindData(aas.Expression(IIF(aas.Data.MainData.Session.DisplayJS, 'active', '')));
rootView.DisplayBindings.BindData(aas.Expression(IIF(aas.Data.MainData.Session.DisplayBindings, 'active', '')));

rootView.Schema.click.BindCommand(aas.Services.Browser.UIService.ShowView(aas.ViewName.SchemaDialog));

rootView.Run.click.BindCommand(aas.Services.Browser.ClientService.Run(aas.Data.MainData, aas.Data.MainData.Session.Id, aas.Data.MainData.Session.Persist));
rootView.Save.click.BindCommand(aas.Services.Browser.ClientService.Run(aas.Data.MainData, aas.Data.MainData.Session.Id, 'force'));

rootView.OnLoad.BindCommand(aas.Services.Server.DataService.LoadSamples(), aas.Data.MainData, true, true);

var mainView = Aspectize.CreateView("MainView", aas.Controls.SideBarContent, aas.Zones.RootView.ZoneMainContent);

mainView.DisplayHTML.BindData(aas.Expression(IIF(aas.Data.MainData.Session.DisplayHTML, '', 'hidden')));
mainView.DisplayCSS.BindData(aas.Expression(IIF(aas.Data.MainData.Session.DisplayCSS, '', 'hidden')));
mainView.DisplayJS.BindData(aas.Expression(IIF(aas.Data.MainData.Session.DisplayJS, '', 'hidden')));
mainView.DisplayBindings.BindData(aas.Expression(IIF(aas.Data.MainData.Session.DisplayBindings, '', 'hidden')));

mainView.UserControl.click.BindCommand(aas.Services.Browser.AceClientService.InsertCode(aas.ViewName.MainView.HTMLEditor, "\n<div aas-control='Test'>\n\n</div>"));
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

var emptyPanel = Aspectize.CreateView("EmptyView", aas.Controls.EmptyControl, aas.Zones.MainView.ZoneResult, true);

var schemaDialog = Aspectize.CreateView("SchemaDialog", aas.Controls.Dialog);
schemaDialog.WithCloseButton.BindData(true);

var schemaImage = Aspectize.CreateView("SchemaImage", aas.Controls.SchemaImage, aas.Zones.SchemaDialog.Dialog, true);
schemaImage.OnLoad.BindCommand(aas.Services.Browser.ClientService.InitDialogImage());
schemaImage.OnDeactivated.BindCommand(aas.Services.Browser.ClientService.CloseDialogImage());

