const library = {
    $webApplication: {
        initialize: function (invokeInBackgroundChanged) {
            document.addEventListener('pointerdown', function () {
                window.focus();
            });

            document.addEventListener('visibilitychange', function () {
                dynCall('vi', invokeInBackgroundChanged, [document.hidden]);
            });
        },

        inBackground: function () {
            return document.hidden;
        },

        isMobile: function () {
            const isMobileDevice = /Android|webOS|iPhone|iPad|iPod|BlackBerry|BB|PlayBook|IEMobile|Windows Phone|Kindle|Silk|Opera Mini/i.test(navigator.userAgent);
            return isMobileDevice;
        },
    },

    
    
    InitializeInternal: function (invokeInBackgroundChanged) {
        webApplication.initialize(invokeInBackgroundChanged);
    },
    
    InBackgroundInternal: function () {
        return webApplication.inBackground();
    },

    IsMobileInternal: function () {
        return webApplication.isMobile();
    },
}

autoAddDeps(library, '$webApplication');
mergeInto(LibraryManager.library, library);