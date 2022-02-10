/*************************
 * 최종수정일 : 2016-06-01
 * 작성자 : devchanho
 * 파일명 : Singleton.cs
 *
 * 일반 클래스 전용 싱글톤 클래스.
 * 이 클래스를 상속받은 클래스는 싱글톤이 된다.
 * MonoBehaviour 전용 클래스는 MonoSingleton 사용.
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
