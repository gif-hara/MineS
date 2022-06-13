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
			var value = allocate(intArrayFromString(err?err:""), 'i8', ALLOC_NORMAL);
			Runtime.dynCall('vii', FileSystem.callback, [id, value]);
		});
	},
};
autoAddDeps(LibraryFileSystem, '$FileSystem');
mergeInto(LibraryManager.library, LibraryFileSystem);