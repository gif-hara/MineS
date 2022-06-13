using AOT;
using System;
using System.Collections.Generic;

namespace WebGL
{
    public static class FileSystem
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        static extern void FileSystemSyncfsAddEvent(Action<int, string> action);
        [DllImport("__Internal")]
        static extern void FileSystemSyncfs(int id);
#else

        static Action<int, string> _callback;
        static void FileSystemSyncfsAddEvent(Action<int, string> action) { _callback = action; }
        static void FileSystemSyncfs(int id) { _callback?.Invoke(id, ""); }
#endif


        static Dictionary<int, Action<string>> callbacks = new Dictionary<int, Action<string>>();

        /// <summary>
        /// 静的コンストラクタ
        /// 同期完了時に呼び出し内部コールバックを登録する
        /// </summary>
        static FileSystem()
        {
            FileSystemSyncfsAddEvent(Callback);
        }

        /// <summary>
        /// 同期完了時の内部コールバック
        /// </summary>
        /// <param name="id"></param>
        [MonoPInvokeCallback(typeof(Action<int, string>))]
        static void Callback(int id, string error)
        {
            var cb = callbacks[id];
            callbacks.Remove(id);
            cb?.Invoke(string.IsNullOrEmpty(error) ? null : error);
        }

        /// <summary>
        /// IndexedDBを同期させる
        /// </summary>
        /// <param name="cb">string : error</param>
        public static void Syncfs(Action<string> cb)
        {
            var id = callbacks.Count + 1;
            callbacks.Add(id, cb);
            FileSystemSyncfs(id);
        }
    }
}
