using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class NewSceneTransition : MonoBehaviour
{
    
    public Animator anim; 
    [SerializeField] string sceneName;


    public void LoadScene()
    {
        StartCoroutine(WaitToLoadScene());
          
    }
    
    private IEnumerator WaitToLoadScene()
    {
        
        anim.SetTrigger("End");
        yield return new WaitForSeconds(3f);
        Debug.Log("Wow we waited for 3 seconds!");
        SceneManager.LoadScene(sceneName);
    }
    
}
