using UnityEngine;

public class FirebaseController : MonoBehaviour
{
    public static FirebaseController instance;

    public FirebaseAuthController authController;
    public FirebaseDatabaseController dbController;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            authController = gameObject.AddComponent<FirebaseAuthController>();
            dbController = gameObject.AddComponent<FirebaseDatabaseController>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
