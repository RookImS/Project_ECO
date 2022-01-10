/*************************
 * ���������� : 2016-06-01
 * �ۼ��� : devchanho
 * ���ϸ� : Singleton.cs
 *
 * �Ϲ� Ŭ���� ���� �̱��� Ŭ����.
 * �� Ŭ������ ��ӹ��� Ŭ������ �̱����� �ȴ�.
 * MonoBehaviour ���� Ŭ������ MonoSingleton ���.
 *************************/

public class Singleton<T> where T : class, new()
{
    private static T _instance;

    private static object _lock = new object();

    public static T Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new T();
                }

                return _instance;
            }
        }
    }

    protected Singleton() { }
}
