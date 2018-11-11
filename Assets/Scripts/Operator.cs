using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

[System.Serializable]
public class Message
{
    // A unique ID for this message (used for completing it)
    public string ID;

    // The message
    public string message;

    // Is this message an objective, or just a message?
    public bool isObjective = false;

    // An optional time-delay before displaying this message after the previous one is dismissed
    public float timeDelay = 0f;

    // Optional prerequisite message/objective that must be completed before this one can be completed (Does not affect showing this message)
    public string prerequisiteID = "";

    // Has this message been completed / dismissed?
    public bool completed = false;
}

public class Operator : MonoBehaviour {
	[SerializeField] private Text operatorText;
	[SerializeField] private GameObject sceneController;
    [SerializeField] private Message[] messages;
	[SerializeField] private float characterDelay = 0.1f;
    [SerializeField] private float interMessageDelay = 0.25f;
    [SerializeField] private string messageCompletedComment = "";

    private bool typing = false;
    private int currentMessage = -1;
    private int typedLength = 1;

    private void Start()
    {
        this.nextMessage();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (typing)
            {
                this.skipTyping();
            } else if (!this.getCurrentMessage().isObjective || this.getCurrentMessage().completed)
            {
                this.attemptCompleteMessage();
            }
        }
    }

    public Message getCurrentMessage()
    {
        return this.messages[this.currentMessage];
    }

    private Message getMessage(string id)
    {
        foreach (Message m in this.messages)
        {
            if (m.ID == id)
            {
                return m;
            }
        }
        return null;
    }

    private void nextMessage()
    {
        this.currentMessage++;
        // If we've shown all of the messages, we're done
        if (this.currentMessage >= this.messages.Length)
        {
            return;
        }

        Message message = this.getCurrentMessage();
        if (message.completed)
        {
            message.message = message.message.Substring(0, (int)Mathf.Floor((float)(message.message.Length * 0.75))) + this.messageCompletedComment;
        }
        operatorText.text = "";
        Invoke("animateDisplayMessage", message.timeDelay);
    }

    private void animateDisplayMessage()
    {
        this.typing = true;
        this.typedLength = 1;
        this.typeMessage();
    }

    private void typeMessage()
    {
        this.operatorText.text = this.getCurrentMessage().message.Substring(0, this.typedLength);
        this.typedLength++;
        if (typing && this.typedLength <= this.getCurrentMessage().message.Length)
        {
            Invoke("typeMessage", this.characterDelay);
        }
        else if (!typing)
        {
            this.operatorText.text = this.getCurrentMessage().message;
            this.typedLength = this.getCurrentMessage().message.Length;
        } else {
            typing = false;
        }
    }

    public bool attemptCompleteMessage()
    {
        return this.completeMessage(this.getCurrentMessage());
    }

    public bool attemptCompleteMessage(string id)
    {
        return this.completeMessage(this.getMessage(id));
    }

    private bool completeMessage(Message message)
    {
        // Decide whether we can complete this message or not (and set completed if we can)
        if (message.prerequisiteID != "")
        {
            if (!this.getMessage(message.prerequisiteID).completed)
            {
                return false;
            } else
            {
                message.completed = true;
            }
        } else
        {
            message.completed = true;
        }

        // Advance the message if we just completed the current message
        if (message == this.getCurrentMessage())
        {
            this.typing = false;
            Invoke("nextMessage", this.interMessageDelay);
        }

        return true;
    }

    public void skipTyping()
    {
        this.typing = false;
    }
}
