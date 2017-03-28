using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

namespace PInvokeLib
{
    public class Caller : DownCall
    {
        private delegate void callback1(
                    [MarshalAs(UnmanagedType.IInspectable)] ref object o,
                    [MarshalAs(UnmanagedType.I8)] long i,
                    [MarshalAs(UnmanagedType.LPWStr)] string a
                    );

        [DllImport("CPPTarget.dll", CharSet = CharSet.Unicode, EntryPoint = "InitCallbacks", CallingConvention = CallingConvention.Cdecl)]
        private static extern int initCallbacks(MulticastDelegate callback);

        [DllImport("CPPTarget.dll", CharSet = CharSet.Unicode, EntryPoint = "DownCall1Target", CallingConvention = CallingConvention.Cdecl)]
        private static extern int downCall1Target([MarshalAs(UnmanagedType.IInspectable)] ref object o, long i, string s);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr LoadLibrary(string dllToLoad);

        public static void Initialize()
        {
            if (Environment.Is64BitProcess)
            {
                IntPtr p = LoadLibrary("D:\\Downloads\\shib\\IIS\\x64\\Debug\\CPPTarget.dll");
            }
            else
            {
                IntPtr p2 = LoadLibrary("D:\\Downloads\\shib\\IIS\\Debug\\CPPTarget.dll");
            }
            initCallbacks(new callback1(upCall1Target));
        }

        private static void upCall1Target([MarshalAs(UnmanagedType.IInspectable)] ref object o, long i, string a)
        {
            UpCall u = (UpCall)o;
            u.upCall1(i, a);
        }


        public Caller(UpCall upCaller)
        {
            m_upCall = upCaller;
        }

        private UpCall m_upCall;
 
        public void downCall1(long i, string a)
        {
            object o = m_upCall;
            downCall1Target(ref o, i, a);
        }

    }
}
