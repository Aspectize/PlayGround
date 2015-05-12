using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Aspectize.Core;
using Aspectize.Web;
using System.Text.RegularExpressions;

namespace PlayGround
{
    [Service(Name = "URLRewritingService")]
    public class URLRewritingService : IURLRewritingService //, IInitializable, ISingleton
    {
        List<RewriteOrRedirect> IURLRewritingService.GetTranslators()
        {
            var translators = new List<RewriteOrRedirect>();

            var pattern = "/playground/id=(.*$)";

            var reg = new Regex(pattern, RegexOptions.IgnoreCase);

            translators.Add((Uri url, ref bool redirect) =>
            {
                redirect = false;

                var m = reg.Match(url.OriginalString);

                if (m.Success)
                {
                    if (!m.Groups[1].Value.ToLower().StartsWith("app.ashx"))
                    {
                        var sessionId = m.Groups[1].Value;

                        var redirectUrl = "/PlayGround/app.ashx?@ClientService.GetSession&id=" + sessionId;

                        redirect = false;

                        var returnUrl = reg.Replace(url.AbsoluteUri, redirectUrl);

                        return returnUrl;
                    }
                    else return url.AbsoluteUri;
                }
                else return null;

            });

            return translators;
        }

    }


}
