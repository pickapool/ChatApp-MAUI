window.onResizeGetWidth = (dotNetHelper) => {
    window.addEventListener('resize', () => {
        const width = window.innerWidth;
        dotNetHelper.invokeMethodAsync('OnResizeWidthCaptured', width);
    });
};