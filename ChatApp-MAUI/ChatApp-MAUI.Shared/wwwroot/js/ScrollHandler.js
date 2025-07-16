window.scrollToBottom = function () {
    var element = document.getElementById("conversation");
    if (element)
        element.scrollIntoView({ behavior: 'smooth' });
};