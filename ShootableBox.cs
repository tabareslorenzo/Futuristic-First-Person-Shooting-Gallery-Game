using UnityEngine;
using System.Collections;


public class ShootableBox : MonoBehaviour {

	//The box's current health point total
	public int currentHealth = 100000;
	public float force = 2000000f;
	/*Random rndcolor = new Random();
	float randonRed = rndcolor.Next(0, 100);
	float randonGreen = rndcolor.Next(0, 100);
	float randonBlue = rndcolor.Next(0, 100); */
	//Color rancolor = new Color();
	public Rigidbody rb;
	public Rigidbody rb1;
	public Vector3 velocity;
	public bool countdown = false;
	public float dur =0.1f;
	public float timer = 0;
	//public float dur2 =5f;
	public float timer2 = 0;
	public float timer3 = 0;
	public float lifetime;
	bool check = false;
	public float count = 0;
	RayCastShootComplete.message[] mess;

	// Using a method
	public void ImportData(RayCastShootComplete source)
	{
		this.mess = source.ExportData();
	}

	//Transform.localScale += new Vector3(0.2F, 0.2F, 0.2F)
	void Start ()
	{
		rb = gameObject.GetComponent<Rigidbody>();
		//rb.velocity.magnitude = rb.velocity.magnitude + 10;
		rb.velocity = rb.velocity *3;
	}

	public void finalMoments()
	{

			Color mycolor = gameObject.GetComponent<Renderer>().material.color;
			if(count>0)
			{
			timer3 += Time.deltaTime;
			if(timer3>dur*100)
			{
				count = 0;
				timer3 =0;
			}
			}
		if(currentHealth == 0 || check)
		{
			if(countdown || check)
			{


				//gameObject.GetComponent<Renderer>().material.color =  Color.black;
				mycolor = gameObject.GetComponent<Renderer>().material.color;
				timer += Time.deltaTime;
				timer2 += Time.deltaTime;

   				if(timer < dur)
   				{
   					gameObject.GetComponent<Renderer>().material.color = Color.white;
   				}

   				if(timer > dur)
   				{
   					gameObject.GetComponent<Renderer>().material.color = Color.black;

   				}
				if(timer >= 2*dur)
				{
				timer = 0;
				}

   				if(timer2 > dur*30)
   				{
   					countdown = false;
					timer2 = 0;

   				}

			}
		}

	}

	public float Damage(int damageAmount, float lifetime, int score)
	{

		//gameObject.GetComponent<Renderer>().material.color = Color.black;
		//subtract damage amount when Damage function is called

		//rb = gameObject.GetComponent<Rigidbody>();
		rb.velocity = rb.velocity * 5;
		//rb.velocity = new Vector3(0,1,0);
		Vector3 x = rb.velocity*-1;
		if(currentHealth==1)
		{
			currentHealth -= damageAmount;
			countdown = true;
		}

		finalCountDown(lifetime, countdown);
		if(currentHealth<=0 && countdown == false){
				gameObject.SetActive (false);
			}



		//var targethit = gameObject;

		//Check if health has fallen below zero


		if(countdown)
		{
			check = true;
			countdown = false;
			currentHealth += damageAmount;
		}

		var first = Object.Instantiate (gameObject);
		rb1 = first.GetComponent<Rigidbody>();
		rb1.velocity = x;

		if(check)
		{
			countdown = true;
			currentHealth -= damageAmount;
			check = false;
		}

		Color myColor =  Color.black;
		Color myColor2 =  Color.white;


		Color check1 = first.GetComponent<Renderer> ().material.color;
		if (check1 == myColor || check1 == myColor2)
		{
			first.GetComponent<Renderer> ().material.color = new Color ((float)Random.value, (float)Random.value, (float)Random.value);
		}
		if(countdown == false)
			{
				//countdown = false;
				currentHealth -= damageAmount;
			}



		finalMoments();

		return lifetime;


		//countdown = true;
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name == "Wall02-00m" || col.gameObject.name == "Wall03-00m" || col.gameObject.name == "Wall05-00m")
		{

			if(count ==0)
			{
				//rb.velocity = rb.velocity * 2;
				count++;
			//	Debug.Log("sdkjf");
			}
			//Destroy(col.gameObject);
			Color myColor =  Color.black;
			Color check = GetComponent<Renderer> ().material.color;
			if (check != myColor && check != Color.white && countdown == false)
			{
				gameObject.GetComponent<Renderer> ().material.color = new Color ((float)Random.value, (float)Random.value, (float)Random.value);
			}
			col.gameObject.GetComponent<Renderer>().material.color = new Color((float)Random.value, (float)Random.value, (float)Random.value);
			for (int i = 0; i < 1; i++)
			{
				gameObject.transform.Translate (-col.contacts[i].normal * force * Time.deltaTime);

			}
			/*foreach (ContactPoint contact in col.contacts)
			{
				velocity = Vector3.Reflect(velocity, contact.normal);
				gameObject.transform.Translate(velocity * Time.deltaTime * force);
			}*/
		}


	}

	void finalCountDown(float lifetime, bool countdown)
	{

		if (lifetime == 20f)
		{
			lifetime = lifetime + Time.time;
		}
		else if(countdown)
		{
			if(lifetime<Time.time)
			{

				lifetime = 20f;
				//countdown = false;

			}
		}
	}

	void Update(){

		finalMoments();
		if(countdown)
		{
			finalCountDown(lifetime, countdown);
		}

	}
}

public class NewBehaviourScript: MonoBehaviour
{
	public float movementSpeed = 10;

	void Update(){

		transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
		//transform.Translate(-hit.normal * movementSpeed * Time.deltaTime);

	}
}
/*
public class DestroyCubes : MonoBehaviour
{
	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name == "prop_powerCube")
		{
			Destroy(col.gameObject);
		}
	}
}
*/
