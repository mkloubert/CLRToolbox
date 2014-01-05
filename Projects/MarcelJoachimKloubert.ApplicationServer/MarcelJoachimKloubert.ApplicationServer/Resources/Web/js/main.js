
var TMAppServer = {};

// events
{
    TMAppServer.events = {};

    TMAppServer.events.pageLoaded = function (ctx) {

    };
}

// functions
{
    TMAppServer.funcs = {};

    TMAppServer.funcs.page = {};
}

// main menu
{
    TMAppServer.mainMenu = {};

    TMAppServer.mainMenu.add = function (newEntry) {
        var opts = $.extend({

        }, newEntry);

        var nee = $('<li><a></a></li>');
        var link = nee.find('a');

        if (opts.href) {
            link.attr('href', opts.href);
        }

        if (opts.caption) {
            if (opts.captionIsHtml) {
                link.html(opts.caption);
            }
            else {
                link.text(opts.caption);
            }
        }

        $('#mainMenuEntries').append(nee);
        return this;
    };
}

// vars
{
    TMAppServer.vars = {};

    TMAppServer.vars.page = {};
}

$(document).ready(function () {
    var pageLoaded = TMAppServer.events.pageLoaded;
    if (pageLoaded) {
        pageLoaded({
        });
    }
});
