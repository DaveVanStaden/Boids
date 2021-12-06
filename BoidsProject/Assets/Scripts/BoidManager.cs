using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    [Range(0f, 3f)]
    public float cohesionVallue = 0.3f;
    [Range (0f,3f)]
    public float seperationVallue = 0.4f;
    [Range(0f, 3f)]
    public float alignmentVallue = 1;

    public int boidsAmount = 40;
    protected List<Boid> allBoids = new List<Boid>();
    [SerializeField] private Boid boidPrefab;
    private void Start()
    {
        for (int i = 0; i < boidsAmount; i++)
        {
            var pos = UnityEngine.Random.insideUnitCircle * 5f;
            allBoids.Add(Instantiate(boidPrefab, pos, Quaternion.identity));
        }

    }
    private void Update()
    {
        for (int i = 0; i < boidsAmount; i++)
        {
            Boid boid = allBoids[i];

            Vector3 r1 = Cohesion(boid);
            Vector3 r2 = Seperation(boid);
            Vector3 r3 = Alignment(boid);

            boid.desiredVelocity = r1 + r2 + r3;
        }
        for (int i = 0; i < allBoids.Count; i++)
        {
            Boid boid = allBoids[i];
            boid.OnUpdate();
        }
    }
    Vector3 Cohesion(Boid boid)
    {
        Vector3 middlePoint = Vector3.zero;
        Vector3 combinedBoids = Vector3.zero;
        for(int i = 0; i < boidsAmount; i++)
        {
            if (allBoids[i] == boid){ continue; }
            combinedBoids += allBoids[i].transform.position;
        }
        middlePoint = combinedBoids / (allBoids.Count - 1);
        return (middlePoint - boid.transform.position) * cohesionVallue;
    }
    Vector3 Seperation(Boid boid)
    {
        Vector3 combinedBoids = Vector3.zero;
        for (int i = 0; i < boidsAmount; i++)
        {
            if (allBoids[i] == boid) { continue; }
            if((boid.transform.position - allBoids[i].transform.position).magnitude < 15)
            {
                combinedBoids = combinedBoids - (allBoids[i].transform.position - boid.transform.position);
            }
        }
        
        return combinedBoids * seperationVallue;
    }
    Vector3 Alignment(Boid boid)
    {
        Vector3 midVelocity= Vector3.zero;
        Vector3 combinedBoids = Vector3.zero;
        for (int i = 0; i < boidsAmount; i++)
        {
            if (allBoids[i] == boid) { continue; }
            combinedBoids += allBoids[i].velocity;
        }
        midVelocity += combinedBoids / (boidsAmount - 1);
        return midVelocity * alignmentVallue;
    }
}