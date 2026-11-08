using UnityEngine;

public class SceneManagement : Singleton<SceneManagement> {
    public string SceneTransitionName { get; private set; }

    // protected override void Awake() {
    //     base.Awake();
    // }

    public void SetTransitionName(string transitionName) {
        this.SceneTransitionName = transitionName;
    }
}