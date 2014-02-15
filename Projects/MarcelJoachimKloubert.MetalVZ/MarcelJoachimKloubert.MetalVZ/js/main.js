
MetalVZ = {};

// functions
MetalVZ.funcs = {};
{

}

// page
MetalVZ.page = {};
{
    MetalVZ.page.events = {};

    MetalVZ.page.request = {};
    {
        // collect HTTP vars (GET)
        {
            MetalVZ.page.request.GET = {};

            document.location.search.replace(/\??(?:([^=]+)=([^&]*)&?)/g, function () {
                var url_decode = function (s) {
                    return decodeURIComponent(s.split('+').join(' '));
                };

                MetalVZ.page.request.GET[mvzUrlDecode(arguments[1])] = url_decode(arguments[2]);
            });
        }
    }

    MetalVZ.page.vars = {};
}

$(document).ready(function () {
    if (MetalVZ.page.events.loaded) {
        var ctx = {
            'time': new Date(),
            'request': MetalVZ.page.request,
        };

        MetalVZ.page.events.loaded(ctx);
    }
});
