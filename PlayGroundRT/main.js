


var playgroundSpecial = (function () {

    var scriptMan = null;

    function newScriptManager() {

        var allScripts = [];

        function newState() {

            var done = false;

            return {

                IsReady: function () { return done; },

                Finished: function (r) { done = true; }
            }
        }

        function loadScript(url) {

            var state = newState();

            allScripts.push(state);

            var head = document.getElementsByTagName('head')[0];

            var s = document.createElement('script');
            s.type = "text/javascript"; s.language = "javascript"; s.src = url;

            s.addEventListener('load', function () { state.Finished(); }, false);

            head.appendChild(s);
        }

        function callContinuation(next) {

            var ready = true;

            for (var n = 0; n < allScripts.length; n++) {

                ready = ready && allScripts[n].IsReady();

                if (!ready) break;
            }

            if (ready) { next(); }

            return ready;
        }

        return {

            Load: loadScript,

            Then: function (next) {

                var done = false;
                var delay = 0;

                function whileNotDone() {

                    try {
                        done = callContinuation(next);
                    } catch (x) {
                        done = true;
                        Aspectize.ReportException(x);
                    }

                    if (!done) {
                        window.setTimeout(whileNotDone, delay); delay += 50;
                    }
                }

                whileNotDone();
            }
        };
    }
    /*
    function decodeUrl(url) {

        var info = {};

        var urlDecoder = /^(\w+):\/\/(?:(.+?)\/)(?:(\w+)\/)?(?:(\w+)\/)app\.ashx/;
        var match = urlDecoder.exec(url);

        if (match) {

            info.HostPath = match[1] + "://" + match[2] + '/';
            if (match[3]) info.HostPath += match[3] + "/";

            info.ApplicationName = match[4];

            info.ApplicationUrl = info.HostPath + info.ApplicationName + "/";

            return info;

        } else return null;
    }
*/
    function init(initSvcName, versionKey) {

        var host = Aspectize.Host;
        hst.ResourcesServiceName = initSvcName;

        //var url = window.location.href;

        //var info = decodeUrl(url);

        //if (info) {

        //var parts = url.split('?');
        //if (parts.length === 2) {
        if (versionKey) {
            //var args = parts[1];
            //var rxArg = /@Id\s*=\s*(\S+)/i;

            //var m = rxArg.exec(args);

            //if (m !== null) {

            window.AspectizeBuildDynamicViews = null;

            var cmdExt = ".js.cmd.ashx?versionKey=" + versionKey;

            var cmdLib = info.ApplicationUrl + initSvcName + '.LoadJsLibrary' + cmdExt;
            var cmdViews = info.ApplicationUrl + initSvcName + '.LoadJsViews' + cmdExt;
            var cmdMain = info.ApplicationUrl + initSvcName + '.LoadMain' + cmdExt;

            var scriptMan = newScriptManager();

            scriptMan.Load(cmdLib);
            scriptMan.Load(cmdViews);
            scriptMan.Load(cmdMain);
        }
        //}
        //}
    }
    function run(next) {

        if (scriptMan) scriptMan.Then(next);
    }

    return { Init: init, Run: run };

})();

function OnApplicationStart() {

    playgroundSpecial.Init('AppInitService', host.UrlArgs.Id);
}

function Main() {

    playgroundSpecial.Run(function () {

        //Aspectize.Host.InitApplication();
        //Aspectize.Host.ActivateViewByName('MainView');
        sMain();

        var style = document.createElement('style');
        style.type = 'text/css';
        style.innerHTML = Aspectize.Host.ExecuteCommand('Server/AppInitService.LoadCSS', Aspectize.Host.UrlArgs.Id);
        document.getElementsByTagName('head')[0].appendChild(style);
    });
}
