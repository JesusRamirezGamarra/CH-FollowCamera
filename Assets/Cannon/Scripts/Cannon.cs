using UnityEngine;
using System.Collections;
using System.Reflection; //make sure you add this!

public class Cannon : MonoBehaviour
{
	public GameObject objetoAInstanciar;
    public Transform lugar ;
	public float timeOfShot = 3f;

	public float speed = 3f;

	public AudioSource audioSource;	
	public AudioSource audioSourceMovePlayer;	

	public GameObject camOne;
	public GameObject camTwo;


	#region private Global Variable
	private bool canShoot = true;
	private float CooldownDuration = 1.0f;
	private const float plusBullets = .75f;
	private float resetTime = 0f;
	private float tempElapsedSeconds =0f;
	private Vector3 directionPlayer;

	#endregion 


	
    private void Update()
    {
		//temporizador();
		// behavior();
		moveAxis();
		changeCamare();
		if (canShoot && Input.GetKeyDown(KeyCode.Space)){
			shoot(1);
		}
		if (Input.GetKeyDown(KeyCode.L)){
			toggleCamera();
		}

    }

	#region Init Variables
	private void configReset(){
		timeOfShot = resetTime;
		tempElapsedSeconds = timeOfShot;
	}
	#endregion
	#region Clear Debug.Log
	public void ClearLog() //you can copy/paste this code to the bottom of your script
	{
		var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
		var type = assembly.GetType("UnityEditor.LogEntries");
		var method = type.GetMethod("Clear");
		method.Invoke(new object(), null);
	}
	#endregion


	private void shoot(int NumberOfBullets)
	{
		canShoot = false;
		for(int i = 0; i < NumberOfBullets; i++){
			Instantiate(	objetoAInstanciar,
								new Vector3(	lugar.position.x-(plusBullets*i), 
												lugar.position.y-(plusBullets*i),
												lugar.position.z-(plusBullets*i)),
								transform.rotation	);	
			audioSource.Play();

		}
		// start the cooldown timer
        StartCoroutine(startCooldown(CooldownDuration));			
	}	

	private IEnumerator startCooldown(float CooldownDuration)
	{
		yield return new WaitForSeconds(CooldownDuration);
		canShoot = true;
	}

	private void moveAxis(){
		// float axisHorizontal = Input.GetAxis("Horizontal");
		// float axisVertical = Input.GetAxis("Vertical");
		float axisHorizontal = Input.GetAxisRaw("Horizontal");  // Round
		float axisVertical = Input.GetAxisRaw("Vertical");		// Round
		Vector3 direction = new Vector3(axisHorizontal,0,axisVertical);
		Vector3 translateDirection = direction * speed * Time.deltaTime;
		transform.Translate(translateDirection);

		if ( Input.GetKey(KeyCode.Q)){
			transform.Rotate(0,-0.1f,0);
		}
		if( Input.GetKey(KeyCode.E)){
			transform.Rotate(0,0.1f,0);
		}


		if (audioSourceMovePlayer.isPlaying && translateDirection == Vector3.zero){
			audioSourceMovePlayer.Stop();
		}
		else {
			while (!audioSourceMovePlayer.isPlaying )
			{
				audioSourceMovePlayer.Play();
			}
		}
	}

	private void changeCamare() {
		if(Input.GetKeyDown(KeyCode.J)){
			Debug.Log("J");
			camOne.SetActive(false);
			camTwo.SetActive(true);
		}
		else if(Input.GetKeyDown(KeyCode.K)){
			Debug.Log("K");
			camOne.SetActive(true);
			camTwo.SetActive(false);
		}
	}

	void toggleCamera() {
		if(camOne.activeInHierarchy){
			camOne.SetActive(false);
			camTwo.SetActive(true);
		}else{
			camOne.SetActive(true);
			camTwo.SetActive(false);
		}
	}
}
