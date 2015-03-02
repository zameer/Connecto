function showNotification(params) {
    var options = {
        'showAfter': 0, // number of sec to wait after page loads
        'duration': 0, // display duration
        'autoClose': false, // flag to autoClose notification message
        'showProgressBar': false,
        'type': 'success', // type of info message error/success/info/warning
        'message': ''// message to dispaly
    };

    // Extending array from params
    $.extend(true, options, params);

    var alertType = 'alert-success'; // default success message will shown
    if (options['type'] == 'error') {
        alertType = 'alert-danger'; // over write the message to error message
    } else if (options['type'] == 'success') {
        alertType = 'alert-success'; // over write the message to information message
    }
    
    var container = "<div id='alert-bar' class='alert " + alertType + "'> <button type='button' class='close' data-dismiss='alert'><span aria-hidden='true'>&times;</span><span class='sr-only'>Close</span></button>" + options['message'] + "</div>";

    $notification = $(container);
    $('div#alert-bar').remove();
    // Appeding notification to Body
    $('body').append($notification);

    var divHeight = $('div#alert-bar').height();
    // see CSS top to minus of div height
    $('div#alert-bar').css({
        top: '-' + divHeight + 'px'
    });

    // showing notification message, default it will be hidden
    $('div#alert-bar').show();

    // Slide Down notification message after startAfter seconds
    slideDownNotification(options['showAfter'], options['autoClose'], options['duration']);
}

function closeNotification(duration) {
    var divHeight = $('div#alert-bar').height();
    setTimeout(function () {
        $('div#alert-bar').animate({
            top: '-' + divHeight
        });
        // removing the notification from body
        setTimeout(function () {
            $('div#alert-bar').remove();

        }, 200);
    }, parseInt(duration * 1000));
}

// sliding down the notification
function slideDownNotification(startAfter, autoClose, duration) {
    setTimeout(function () {
        $('div#alert-bar').animate({
            top: 0
        });
        if (autoClose) {
            setTimeout(function () {
                closeNotification(duration);
            }, duration);
        }
    }, parseInt(startAfter * 1000));
}




