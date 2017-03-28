// CPPTarget.cpp : Defines the exported functions for the DLL application.
//

#include <windows.h>

typedef
VOID
(*CALLBACK1)(
    _In_ PVOID CSharpObject,
    _In_ DWORD64 Int,
    _In_ PWCHAR String
    );

CALLBACK1 upCall1Target;


class doitClass {
public:
    doitClass(PVOID what) : m_cSharpObject(what)
    {};

    VOID Call1(DWORD64 i, PWCHAR s)
    {
        upCall1Target(m_cSharpObject, i, s);
    }
private:
    PVOID m_cSharpObject;
};


extern "C" {
    _declspec(dllexport)
    _Must_inspect_result_
    _Success_(S_OK == return)
    DWORD
    _cdecl
    InitCallbacks(CALLBACK1 c1)
    {
        upCall1Target = c1;
        return S_OK;
    }

    _declspec(dllexport)
    VOID
    _cdecl
    DownCall1Target(PVOID obj, DWORD64 i, PWCHAR s)
    {
        doitClass doit(obj);

        doit.Call1(i, s);
    }

}