using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class All : MonoBehaviour
{

    public GameObject player1; //gameobject for the player
    public GameObject player2; //gameobject for cpu1
    public GameObject player3; //gameobject for cpu2
    public GameObject player4; //gameobject for cpu3

    public Rigidbody playerRb1; // Rigidbody for player 1
    public Rigidbody playerRb2; // Rigidbody for player 2
    public Rigidbody playerRb3; // Rigidbody for player 3
    public Rigidbody playerRb4; // Rigidbody for player 4

    public Material Three; //material for 3 object
    public Material Two; //material for 2 object
    public Material One; //material for 1 object

    public Text FirstText; //text that shows who came first
    public Text SecondText; //text that shows who came second
    public Text ThirdText; //text that shows who came third
    public Text FourthText; //text that shows who came fourth
    public Text TimerText; //text that shows the time
    public Text TopTimeText; //text that shows the top time
    public Text LastText; //text that shows the last time

    public Button UnoPlayer; //button for singleplayer game
    public Button DosPlayer; //button for twoplayer game
    public Button TresPlayer; //button for threeplayer game
    public Button CuatroPlayer; //button for fourplayer game

    public Image OnePlayer; //Image for UnoPlayer
    public Image TwoPlayer; //Image for DuoPlayer
    public Image ThreePlayer; //Image for TresPlayer
    public Image FourPlayer; //Image for CuatroPlayer

    public Button back; //Button to move form namechange to main

    public InputField TopRacerText;
    public InputField SecondRacerText;
    public InputField ThirdRacerText;
    public InputField FourthRacerText;

    public Text TopRacerName;
    public Text SecondRacerName;
    public Text ThirdRacerName;
    public Text FourthRacerName;

    public Button ChangeName;


    public Button PlayButton; //button to start the game
    public Canvas StartCanvas; //canvas before game has started
    public Canvas GameCanvas; //canvas when game has started
    public Canvas NameChangerCanvas; //canvas when names are changed has started

    public AudioClip BeepAudio;
    private AudioSource audioSource;


    string[] names = { PlayerPrefs.GetString("TopRacer"), PlayerPrefs.GetString("2ndRacer"), PlayerPrefs.GetString("3rdRacer"), PlayerPrefs.GetString("4thRacer")};

    int hasstarted; //shows if the game has started
    int timer; //value for the time
    int toptime; //value for toptime

    int numplayers; //value for number of players

    string[] results = { "Player1", "Player2", "Player3", "Player4" }; //names of all racers
    public float[] finishposition = { 0f, 0f, 0f, 0f }; //shows the current position of the racers

    float beguncount; //used to show the scale of 3,2,1 when the game has started
    int counter; //used as a counter to find rate of space press
    int[] lastpressed = { -1000, -1000, -1000, -1000 }; //used to show the last time the space bar was pressed

    public float speed; //used to show the speed of the player
    float randomspeed2;
    float randomspeed3;
    float randomspeed4;

    public int endpoint; //used to show the endposition

    int top; //shows the top value of the random values
    int bottom; //shows the bottom value of the random values

    // Start is called before the first frame update
    void Start()
    {


        if (PlayerPrefs.GetInt("toptime") == 0) { PlayerPrefs.SetInt("toptime", 100000); TopTimeText.text = ""; } //sets toptime too large val if never run before
        else { TopTimeText.text = "Top Time: " + PlayerPrefs.GetInt("toptime").ToString(); } //otherwise sets it to top time

        //initialises all values
        numplayers = 1;
        beguncount = 0;
        counter = 0;
        hasstarted = 0;
        StartCanvas.enabled = true;
        GameCanvas.enabled = false;
        NameChangerCanvas.enabled = false;
        Three.color = new Color(0.5f, 0.5f, 0.5f, 1);
        Two.color = new Color(0.5f, 0.5f, 0.5f, 1);
        One.color = new Color(0.5f, 0.5f, 0.5f, 1);

        TopRacerText.text = PlayerPrefs.GetString("TopRacer");
        SecondRacerText.text = PlayerPrefs.GetString("2ndRacer");
        ThirdRacerText.text = PlayerPrefs.GetString("3rdRacer");
        FourthRacerText.text = PlayerPrefs.GetString("4thRacer");

        // Adds random values to CPU speeds to appear slower or faster
        randomspeed2 = Random.Range(2.5f, 10f);
        randomspeed3 = Random.Range(2.5f, 10f);
        randomspeed4 = Random.Range(2.5f, 10f);

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape")) { Application.Quit(); }
        if (hasstarted == 1) //if the game has started but the race has not
        {

            if (beguncount == 0) { Three.color = new Color(1, 0, 0, 1); audioSource.PlayOneShot(BeepAudio, 1.0f); } //changes 3 colour depending on how long time has passed
            else if (beguncount == 100) { Two.color = new Color(1, 0, 0, 1); audioSource.PlayOneShot(BeepAudio, 1.0f); } //decreases 2 scale depending on how long time has passed
            else if (beguncount == 200) { One.color = new Color(1, 0, 0, 1); audioSource.PlayOneShot(BeepAudio, 1.0f); } //decreases 1 scale depending on how long time has passed
            else if (beguncount == 300)
            {
                Three.color = new Color(0, 1, 0, 1);
                Two.color = new Color(0, 1, 0, 1);
                One.color = new Color(0, 1, 0, 1);
                audioSource.PlayOneShot(BeepAudio, 1.0f);
                hasstarted = 2;
               
                if (numplayers < 4) {
                    playerRb4.AddForce(Vector3.right * speed);

                    if (numplayers < 3) {
                        playerRb3.AddForce(Vector3.right * speed);

                        if (numplayers < 2) {
                            playerRb2.AddForce(Vector3.right * speed);              
}

}
}

            } //starts the game if enough time has passed

            beguncount = beguncount + 1; //increases counter
        }


        else if (hasstarted == 2) //if the race has started
        {


            timer = counter / 20;
            TimerText.text = "Time: " + timer.ToString();

            GameCanvas.enabled = true; //starts showing the game canvas
            GameObject[] players = { player1, player2, player3, player4 }; //holds gameobjects for all players to find positions

            if (Input.GetKeyUp(KeyCode.Space)) { playerRb1.AddForce(Vector3.right * speed); } // adds a force to player

            if (numplayers >= 2)  //sets speed for player 2
            {
                if (Input.GetKeyUp(KeyCode.RightShift)) { playerRb2.AddForce(Vector3.right * speed); } // adds a force to player

                if (numplayers >= 3) //sets speed for player 3
                {
                    if (Input.GetKeyUp(KeyCode.Tab)) { playerRb3.AddForce(Vector3.right * speed); } // adds a force to player

                    if (numplayers == 4) //sets speed for player 4
                    {
                        if (Input.GetKeyUp(KeyCode.Mouse0)) { playerRb4.AddForce(Vector3.right * speed); } // adds a force to player
                    }
                }
            }

            if (numplayers != 4) //every 30 frames
            {
                if (numplayers < 4) {
                    float player4distance = player1.transform.position.x - player4.transform.position.x;
                    if (player4distance < 0) {player4distance = 0;}
                    playerRb4.AddForce(Vector3.right * (0.07f * speed + 0.5f * player4distance + randomspeed4 + Random.Range(1, 3)));
                   
                    if (numplayers < 3) {
                        float player3distance = player1.transform.position.x - player3.transform.position.x;
                        if (player3distance < 0) {player3distance = 0;}
                        playerRb3.AddForce(Vector3.right * (0.07f * speed + 0.5f * player3distance + randomspeed3 + Random.Range(1, 3)));

                        if (numplayers < 2) {
                            float player2distance = player1.transform.position.x - player2.transform.position.x;
                            if (player2distance < 0) {player2distance = 0;}
                            playerRb2.AddForce(Vector3.right * (0.07f * speed + 0.5f * player2distance + randomspeed2 + Random.Range(1, 3)));
 
       }
   }
}
}

         
            //sorting array for current position

            if (player1.transform.position.x >= endpoint) //checks if the player has reached the end
            {

                finishposition[0] = player1.transform.position.x;
                finishposition[1] = player2.transform.position.x;
                finishposition[2] = player3.transform.position.x;
                finishposition[3] = player4.transform.position.x;

                for (int j = 0; j < finishposition.Length; j++)
                {
                   for (int k = 0; k < finishposition.Length - 1; k++)
                    {
                        if (finishposition[k] < finishposition[k + 1])
                        {
                            float filler = finishposition[k + 1];
                            finishposition[k + 1] = finishposition[k];
                            finishposition[k] = filler;
                            string fillerstr = results[k + 1];
                            results[k + 1] = results[k];
                            results[k] = fillerstr;
                        }
                    }
                }


                if (timer < PlayerPrefs.GetInt("toptime")) { PlayerPrefs.SetInt("toptime", timer);  } //resets toptime if faster

                PlayerPrefs.SetString("First", results[0]);
                PlayerPrefs.SetString("Second", results[1]);
                PlayerPrefs.SetString("Third", results[2]);
                PlayerPrefs.SetString("Fourth", results[3]);
                PlayerPrefs.SetInt("LastTime", timer);

                TopTimeText.text = "Top Time: " + toptime.ToString();
                SceneManager.LoadScene(0);
            }

            counter = counter + 1; //increases the counter every frame
        }

        else {        //if the game has not started

            PlayButton.onClick.AddListener(() => startin()); //starts the game if the play button is pressed


            UnoPlayer.onClick.AddListener(() => changenumplayers(1)); //changes number of players to one
            DosPlayer.onClick.AddListener(() => changenumplayers(2)); //changes number of players to two
            TresPlayer.onClick.AddListener(() => changenumplayers(3)); //changes number of players to three
            CuatroPlayer.onClick.AddListener(() => changenumplayers(4)); //changes number of players to four

            back.onClick.AddListener(() => startmenu()); //moves player to start menu

            ChangeName.onClick.AddListener(() => changenames()); //allows player to change names

            PlayerPrefs.SetString("TopRacer", TopRacerText.text);
            PlayerPrefs.SetString("2ndRacer", SecondRacerText.text);
            PlayerPrefs.SetString("3rdRacer", ThirdRacerText.text);
            PlayerPrefs.SetString("4thRacer", FourthRacerText.text);
           
            string[] names = { PlayerPrefs.GetString("TopRacer"), PlayerPrefs.GetString("2ndRacer"), PlayerPrefs.GetString("3rdRacer"), PlayerPrefs.GetString("4thRacer")};
            results = names;

            TopRacerName.text = names[0].ToString();
            SecondRacerName.text = names[1].ToString();
            ThirdRacerName.text = names[2].ToString();
            FourthRacerName.text = names[3].ToString();


            //changes the texts to show the finishing order
            FirstText.text = PlayerPrefs.GetString("First");
            SecondText.text = PlayerPrefs.GetString("Second");
            ThirdText.text = PlayerPrefs.GetString("Third");
            FourthText.text = PlayerPrefs.GetString("Fourth");
            LastText.text = "Last Time: " + PlayerPrefs.GetInt("LastTime").ToString();


        }
    }

    void changenumplayers(int num)
    {
        Image[] buttonimages = { OnePlayer, TwoPlayer, ThreePlayer, FourPlayer };
        for (int i = 0; i < 4; i++) { buttonimages[i].color = new Color(1, 0, 0, 1); }
        buttonimages[num-1].color = new Color(0, 1, 0, 1);
        numplayers = num;
    }



    void startin()
    {
        hasstarted = 1; //starts the game
        counter = 0; //initialises values
        beguncount = 0; //initialises values
        StartCanvas.enabled = false; //closes the start canvas
        GameCanvas.enabled = true; //opens the game canvas
        PlayButton.enabled = false;
    }

    void changenames()
    {
        StartCanvas.enabled = false;
        NameChangerCanvas.enabled = true;
    }

    void startmenu()
    {
        StartCanvas.enabled = true;
        NameChangerCanvas.enabled = false;
    }


}
