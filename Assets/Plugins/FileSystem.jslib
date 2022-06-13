//=====================================================
// JavaScript FileSystem Plugin for UnityWebGL
//=====================================================

var LibraryFileSystem = {
    $FileSystem: {
        callback: null,
    },

    // Add Event Callback
    FileSystemSyncfsAddEvent: function (cb) {
        FileSystem.callback = cb;
    },

	// Sync
	FileSystemSyncfs:function(id) {
		FS.syncfs(function (err) {
		});
	},
};
autoAddDeps(LibraryFileSystem, '$FileSystem');
mergeInto(LibraryManager.library, LibraryFileSystem);