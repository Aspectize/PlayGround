function Main() {

    Aspectize.Host.ResourcesServiceName = "AppInitService";

    var mainView = Aspectize.Host.InitApplication();



    Aspectize.Host.ActivateViewByName('MainView');
}
