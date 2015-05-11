var v = Aspectize.CreateView("hh", aas.Controls.AspectizeFlyOutPanel);

v.OnLoad.BindCommand(aas.Services.Server.LoadDataService.LoadCategories(), aas.Data.AdventureWorksData, true);