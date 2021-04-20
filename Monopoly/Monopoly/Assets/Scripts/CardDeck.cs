using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    
    //private Transform[] childNodeList;
    private Card[] cardList;
    private int currentDeckPosition = 0;
    
    

    // Start is called before the first frame update
    void Start()
    {
        Transform[] childNodeList;
        childNodeList = GetComponentsInChildren<Transform>();
        cardList = new Card[childNodeList.Length];
        for(int i = 0; i < childNodeList.Length; i++){
            if (childNodeList[i] != this.transform){
                cardList[i] = childNodeList[i].gameObject.GetComponent<Card>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //O(N^2) sort function, was lazy while programming....
    public void shuffle() {
        //get randoms for cards
        currentDeckPosition = 0;
        float[] randomRef = new float[cardList.Length];

        for(int i = 0; i < randomRef.Length; i++) 
        {
            randomRef[i] = Random.Range(0, 100);
        }

        //sort list by new randoms
        for (int i = 0; i < cardList.Length; i++) {
            int maxPoint = i;
            float max = 0;
            for (int j = 0; j < cardList.Length; j++) {
                if (randomRef[i] > max){
                    maxPoint = j;
                    max = randomRef[j];
                }
            }
            Card temp = cardList[i];
            cardList[i] = cardList[maxPoint];
            cardList[maxPoint] = temp;
        }
    }

    public Card draw()
    {
        Card returnable = cardList[currentDeckPosition];
        currentDeckPosition++;
        return returnable;
    }

}
