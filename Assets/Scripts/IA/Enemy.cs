using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//4 estados: CHASEAR, COOLDOWN, PATROL, ALERTA
public class Enemy : MonoBehaviour {
    //animator
    public Animator animator;

    public int waypointsIndex = 0;
    public Transform target;
    public GameObject player;
    public Vector3 playerPos;
    public float speed;
    public float radius;
    public float enemyDistance;
    //
    public Renderer me;
    public LayerMask visibles = ~0;
    public bool coolDown;
    public bool caution;
    public bool onSight;
    public bool chase;
    public bool regroup;
    public bool aStar;
    public float timer; //timer en el que se queda patrullando en el lugar y luego vuelve
    public float timerCoolDown;
    //
    public EfeedBack feedback;
    public StateMachine<EnemyInput> stateMachine;
    public string action;
    public float playerAngle;
    public float  playerposDist;
    public static List<Waypoints> cellWaypoints = new List<Waypoints>();
    //public float evasionRadius;
    //public float targetWeight = 0.5f;
    //public float obstacleWeight = 0.5f;
    //private Vector3 dirToTarget;
    //private Vector3 dirFromObstacle;
    //public Vector3 velocity;
    //public Obstacles obstacles;

    void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("FPSController");
        aStar = false;
        chase = false;
        regroup = false;
        //event manager
        playerPos = player.transform.position;
        EventsManager.SubscribeToEvent(EventTypeBoss.GP_UpdatePos, Chase);
        EventsManager.SubscribeToEvent(EventTypeBoss.GP_AlertAllies, AlertAll);
            //state machine
        var chaseState = new EChaseState(this);
        var cooldownState = new ECoolDownState(this);
        var patrolState = new EPatrolState(this);
        var alertState = new EAlertState(this);

        patrolState.AddTransition(EnemyInput.Patrol, chaseState);

        chaseState.AddTransition(EnemyInput.Chase,cooldownState);
        chaseState.AddTransition(EnemyInput.Chase, alertState);

        alertState.AddTransition(EnemyInput.Alert, chaseState);
        alertState.AddTransition(EnemyInput.Alert, cooldownState);

        cooldownState.AddTransition(EnemyInput.CoolDown, patrolState);

     

        stateMachine = new StateMachine<EnemyInput>(patrolState);
    }
    private IEnumerator WaypointDetecter()
    {
        print("sarasa");
        target = Waypoints.waypoints[0];
        newWaypoint();
        yield return new WaitForSeconds(1f);
    }
        void Start() {
        me = GetComponent<Renderer>();
        coolDown = false; timer = 0;
        
        //setear estados, agregar transiciones iniciales para despues utilizarlas
    }

    void Update()
    {

        if (target.ToString() == "Waypoints")
            StartCoroutine(WaypointDetecter());

        playerposDist = Vector3.Distance(this.transform.position, playerPos);
        playerAngle = Vector3.Angle(transform.forward, player.transform.position - transform.position);
        enemyDistance = Vector3.Distance(this.transform.position, player.transform.position);
        var rayDist = player.transform.position - transform.position;
        #region trigger events
        //eventsmanager
        if (action == "Chase") //si ve al player, es decir si esta en "chase" el booleano chase se activa 
            chase = true; //para reagrupar a los enemigos en el ultimo punto donde se vio al player
        //if (enemyDistance < radius) { regroup = false; chase = false; }
        RaycastHit hit;
        if (Physics.Raycast(transform.position, rayDist, out hit, radius, visibles))
        {
        if (enemyDistance < radius && playerAngle < 90 / 2 && !coolDown && hit.transform == player.transform) //si esta en el radio y angulo de vision, y cooldown es falso, actualiza pos y alerta a los demas enemigos
        {
                EventsManager.TriggerEvent(EventTypeBoss.GP_UpdatePos, new object[] { playerPos });
                EventsManager.TriggerEvent(EventTypeBoss.GP_AlertAllies, new object[] { });
        }
            if (hit.transform != player.transform)
            { onSight = false;  } 
            else if (hit.transform == player.transform && enemyDistance < radius && playerAngle < 90 / 2)
            { onSight = true;  }
            else if (enemyDistance > radius) { onSight = false; }
        }
        #endregion
        #region preguntas state machine(correciones para compatibilidad con event manager)
        if (!coolDown && !aStar)
        {

            if (!caution && !onSight && !chase) //va hacia los puntos
            {
                animator.SetBool("AtRange", false);
                Patrol(); stateMachine.Feed(EnemyInput.Patrol); action = "Patrol";
                //Debug.Log("patrulla");
            }
            else if (enemyDistance < radius && enemyDistance > 2 && onSight) //va hacia el enemigo
            {
                animator.SetBool("AtRange", true);
                chase = true;
                regroup = true;
                Chase(); stateMachine.Feed(EnemyInput.Chase); action = "Chase";
            }
            else if (enemyDistance > radius && caution && timer < 2.5f && !onSight) //cuando sale del rango de vision, entra en alerta
            {
                animator.SetBool("Lost", true);
                Alert(); stateMachine.Feed(EnemyInput.Alert); action = "Alert";
            }
            else if (enemyDistance > radius && caution && timer > 2.5f && !onSight) //cuando se cansa de buscarlo
            {
                animator.SetBool("AtRange", false);
                animator.SetBool("Lost", false);
                caution = false;
                timer = 0;
                onSight = false;

            }
            else if (enemyDistance <= 2 && onSight) //explota y entra en cooldown
            {
                animator.SetBool("Attack", true);
                onSight = false;
                coolDown = true;

                caution = false;
            }
            else if (regroup && chase && action != "Chase")
            {
                speed = 8;
                //Debug.Log(gameObject.name + " chasea al player");
                transform.position += transform.forward * speed * Time.deltaTime;
                transform.forward = playerPos - transform.position;
                action = "Regroup";


            }
            if (action == "Alert" && chase) { chase = false; }

            if (playerposDist < 4f && chase)
            {
                //Debug.Log(gameObject.name + " para");
                chase = false;
                regroup = false;
                //caution = false;
            }
        }
        else if (!aStar)
        {


            CoolDown(); stateMachine.Feed(EnemyInput.CoolDown); action = "CoolDown";
        }
      
    }
        #endregion
        #region Funciones event manager
    public void Chase(params object[] parameters)
    {   
        //Debug.Log(gameObject.name + " lo ve y actualiza la pos del player");
        playerPos = player.transform.position;
    }
    public void AlertAll(params object[] parameters)
    {
        action = "Regroup";
        if (action == "Regroup")
            chase = true;

        if(chase && action != "Chase" && enemyDistance > radius)
        {
            regroup = true;
        }   
    }
    #endregion

    #region states
    public void Chase()
    {
        feedback.Chase();
    }
    public void CoolDown()
    {
        feedback.CoolDown();
    }
    public void Patrol()
    {
        feedback.Patrol();
    }
    public void Alert()
    {
        feedback.Alert();
    }
    #endregion

    #region Gizmos,colisiones y función Waypoints
    public void newWaypoint()
    {
        if (waypointsIndex >= Waypoints.waypoints.Length)
        {
            waypointsIndex = 0;
            return; //evita el error array index out of range al llegar al final 
                    //y en este caso hago que se resetee el camino de los waypoints
        }

        target = Waypoints.waypoints[waypointsIndex];
        waypointsIndex++;
    }
    public void OnDrawGizmos()
    {
        if (target != null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= 60)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, radius);
            }
            else { Gizmos.color = Color.yellow; Gizmos.DrawWireSphere(transform.position, radius); }
        }
        var position = transform.position;


        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(position, position + Quaternion.Euler(0, 90 / 2, 0)
            * transform.forward * radius);
        Gizmos.DrawLine(position, position + Quaternion.Euler(0, -90 / 2, 0)
            * transform.forward * radius);

        Gizmos.color = Color.green;
        //Gizmos.DrawLine(transform.position, transform.position + dirToTarget * 5);
        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position, transform.forward);
    }
    public void OnCollisionEnter(Collision a)
    {
        if(a.collider.name == "Player")
        {
            onSight = true;
        }
    }
    public void SearchEnemy()
    {
        Debug.Log("voy");
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("way");
        
        float shortestD = Mathf.Infinity;
        float atRadius = 8f;
        GameObject closestWay = null;
        foreach (GameObject a in waypoints)
        {
            float distanceToWay = Vector3.Distance(transform.position, a.transform.position);
            if (distanceToWay < shortestD)
            {
                shortestD = distanceToWay;
                closestWay = a;
                atRadius = distanceToWay;
            }
        }


        if (closestWay != null && atRadius < radius && target != null)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
            transform.forward = target.transform.position - transform.position;
            target = closestWay.transform;
        }
        else { target = null; return; }
    }
    #endregion
}
