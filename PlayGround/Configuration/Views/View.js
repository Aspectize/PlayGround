
var rootView = Aspectize.CreateView("RootView", aas.Controls.RootControl);
rootView.DisplayHTML.click.BindCommand(aas.Services.Browser.ClientService.TogglePanelAndResize('DisplayHTML', 'HtmlPanel'));
rootView.DisplayMain.click.BindCommand(aas.Services.Browser.ClientService.TogglePanelAndResize('DisplayMain', 'MainPanel'));
rootView.DisplayCSS.click.BindCommand(aas.Services.Browser.ClientService.TogglePanelAndResize('DisplayCSS', 'CssPanel'));
rootView.DisplayJS.click.BindCommand(aas.Services.Browser.ClientService.TogglePanelAndResize('DisplayJS', 'JsPanel'));
rootView.DisplayBindings.click.BindCommand(aas.Services.Browser.ClientService.TogglePanel('DisplayBindings'));

rootView.DisplayHTML.BindData(aas.Expression(IIF(aas.Data.MainData.Session.DisplayHTML, 'active', '')));
rootView.DisplayMain.BindData(aas.Expression(IIF(aas.Data.MainData.Session.DisplayMain, 'active', '')));
rootView.DisplayCSS.BindData(aas.Expression(IIF(aas.Data.MainData.Session.DisplayCSS, 'active', '')));
rootView.DisplayJS.BindData(aas.Expression(IIF(aas.Data.MainData.Session.DisplayJS, 'active', '')));
rootView.DisplayBindings.BindData(aas.Expression(IIF(aas.Data.MainData.Session.DisplayBindings, 'active', '')));

rootView.Schema.click.BindCommand(aas.Services.Browser.UIService.ShowView(aas.ViewName.SchemaDialog));
rootView.StartTourAgain.click.BindCommand(aas.Services.Browser.ClientService.InitWelcome());
rootView.StartTourAgain.click.BindCommand(aas.Services.Browser.ClientService.StartTour(aas.Data.MainData.Session.Id));

rootView.Run.click.BindCommand(aas.Services.Browser.ClientService.Run(aas.Data.MainData, aas.Data.MainData.Session.Id, aas.Data.MainData.Session.Persist));
rootView.Save.click.BindCommand(aas.Services.Browser.ClientService.Run(aas.Data.MainData, aas.Data.MainData.Session.Id, 'force'));

rootView.OnLoad.BindCommand(aas.Services.Server.DataService.LoadSamples(), aas.Data.MainData, true, true);

rootView.StartTour.click.BindCommand(aas.Services.Browser.ClientService.StartTour(aas.Data.MainData.Session.Id));

var mainView = Aspectize.CreateView("MainView", aas.Controls.SideBarContent, aas.Zones.RootView.ZoneMainContent);

mainView.ResultInNexTab.href.BindData(aas.Expression(aas.Services.Browser.ClientService.BuildUrlRT(aas.Data.MainData.Session.CirculatingId)));

mainView.DisplayHTML.BindData(aas.Expression(IIF(aas.Data.MainData.Session.DisplayHTML, '', 'hidden')));
mainView.DisplayMain.BindData(aas.Expression(IIF(aas.Data.MainData.Session.DisplayMain, '', 'hidden')));
mainView.DisplayCSS.BindData(aas.Expression(IIF(aas.Data.MainData.Session.DisplayCSS, '', 'hidden')));
mainView.DisplayJS.BindData(aas.Expression(IIF(aas.Data.MainData.Session.DisplayJS, '', 'hidden')));
mainView.DisplayBindings.BindData(aas.Expression(IIF(aas.Data.MainData.Session.DisplayBindings, '', 'hidden')));

mainView.UserControl.click.BindCommand(aas.Services.Browser.AceClientService.InsertCode(aas.ViewName.HTMLEditor.Editor, "\n<div aas-control='Test'>\n\n</div>"));
mainView.TextBox.click.BindCommand(aas.Services.Browser.AceClientService.InsertCode(aas.ViewName.HTMLEditor.Editor, "<input name='MyText' type='text' />"));
mainView.TextArea.click.BindCommand(aas.Services.Browser.AceClientService.InsertCode(aas.ViewName.HTMLEditor.Editor, "<textarea name='MyTextArea' cols='20' rows='2'></textarea>"));
mainView.Button.click.BindCommand(aas.Services.Browser.AceClientService.InsertCode(aas.ViewName.HTMLEditor.Editor, "<input name='MyButton' type='button' value='MyButton' />"));
mainView.Checkbox.click.BindCommand(aas.Services.Browser.AceClientService.InsertCode(aas.ViewName.HTMLEditor.Editor, "<input name='MyCheckBox' type='checkbox' />"));
mainView.ComboBox.click.BindCommand(aas.Services.Browser.AceClientService.InsertCode(aas.ViewName.HTMLEditor.Editor, "<select name='MySelect'></select>"));

mainView.OnLoad.BindCommand(aas.Services.Server.DataService.LoadSamples(), aas.Data.MainData, true);
mainView.SelectSamples.BindList(aas.Data.MainData.EnumInBaseSample, "Id", "Name", "Name");
mainView.SelectSamples.NullValueDisplay.BindData("Browse samples");
mainView.SelectSamples.SelectedValueChanged.BindCommand(aas.Services.Browser.ClientService.GetSession(mainView.SelectSamples.CurrentValue));
mainView.SelectSamples.SelectedValueChanged.BindCommand(aas.Services.Browser.ClientService.UpdateUrl(mainView.SelectSamples.CurrentValue));
mainView.SelectSamples.DefaultIndex.BindData(0);

var htmlEditor = Aspectize.CreateView("HTMLEditor", aas.Controls.EditorPanel, aas.Zones.MainView.ZoneHtml, true);
htmlEditor.Editor.Mode.BindData('html');
htmlEditor.Editor.Value.BindData(aas.Data.MainData.Session.Html);
htmlEditor.PanelCaption.BindData('HTML');

var mainEditor = Aspectize.CreateView("MainEditor", aas.Controls.EditorPanel, aas.Zones.MainView.ZoneMain, true);
mainEditor.Editor.Value.BindData(aas.Data.MainData.Session.MainJS);
mainEditor.PanelCaption.BindData('Main');

var cssEditor = Aspectize.CreateView("CSSEditor", aas.Controls.EditorPanel, aas.Zones.MainView.ZoneCSS, true);
cssEditor.Editor.Mode.BindData('css');
cssEditor.Editor.Value.BindData(aas.Data.MainData.Session.css);
cssEditor.PanelCaption.BindData('CSS');

var bindingEditor = Aspectize.CreateView("BindingEditor", aas.Controls.EditorPanel, aas.Zones.MainView.ZoneBindings, true);
bindingEditor.Editor.Value.BindData(aas.Data.MainData.Session.Bindings);
bindingEditor.PanelCaption.BindData('Bindings');

var jsEditor = Aspectize.CreateView("JSEditor", aas.Controls.EditorPanel, aas.Zones.MainView.ZoneJS, true);
jsEditor.Editor.Value.BindData(aas.Data.MainData.Session.js);
jsEditor.PanelCaption.BindData('Commands');

var errorResult = Aspectize.CreateView("ErrorResult", aas.Controls.ErrorControl, aas.Zones.MainView.ZoneResult);
errorResult.ErrorMessage.BindData(aas.Data.MainData.Session.Log, "{:S}");

var iFrameView = Aspectize.CreateView("IFrameResult", aas.Controls.IFrameControl, aas.Zones.MainView.ZoneResult);

var emptyPanel = Aspectize.CreateView("EmptyView", aas.Controls.EmptyControl, aas.Zones.MainView.ZoneResult, true);

var schemaDialog = Aspectize.CreateView("SchemaDialog", aas.Controls.Dialog);
schemaDialog.WithCloseButton.BindData(true);
schemaDialog.Text.BindData('AdventureWorks Schemas');

var schemaImage = Aspectize.CreateView("SchemaImage", aas.Controls.SchemaImage, aas.Zones.SchemaDialog.Dialog, true);
schemaImage.OnLoad.BindCommand(aas.Services.Browser.ClientService.InitDialogImage());
schemaImage.OnDeactivated.BindCommand(aas.Services.Browser.ClientService.CloseDialogImage());

