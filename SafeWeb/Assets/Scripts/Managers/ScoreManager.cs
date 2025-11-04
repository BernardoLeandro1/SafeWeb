using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private UIManager uiManager;
    private string reference = "";

    private int value = 0;

    private Dictionary<string, int> scoreSystem = new Dictionary<string, int>();

    private string advices = ""; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreSystem.Add("maria", 25);
        scoreSystem.Add("ana", 25);
        scoreSystem.Add("antonio", 25);
        scoreSystem.Add("joao", 25);
        scoreSystem.Add("pai", 25);
        scoreSystem.Add("conta", 0);
        scoreSystem.Add("clara", 0);
        scoreSystem.Add("jaime", 0);
        scoreSystem.Add("link", 0);
        scoreSystem.Add("post", 0);
        scoreSystem.Add("password", 0);
        scoreSystem.Add("wifi", 0);
        scoreSystem.Add("dicas1", 0);
        scoreSystem.Add("dicas2", 0);
        scoreSystem.Add("dicas3", 0);
        scoreSystem.Add("dicas4", 0);
        scoreSystem.Add("dicas5", 0);
        scoreSystem.Add("dicas6", 0);
        scoreSystem.Add("jaimelink", 0);
        uiManager = GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddScore(string name, int score)
    {
        if (name.Contains("_"))
        {
            foreach (var item in name.Split('_'))
            {
                AddScore(item, score);
            }
        }
        else if (name.Contains("todos"))
        {
            AddScore("maria", score);
            AddScore("ana", score);
            AddScore("antonio", score);
            AddScore("joao", score);
            AddScore("pai", score);
        }
        else if (scoreSystem.ContainsKey(name))
        {
            scoreSystem[name] += score;
        }
        foreach (var person in scoreSystem.Keys)
        {
            Debug.Log("score: " + person + " - " + scoreSystem[person]);
        }
        reference = name;
        value = score;
    }

    public void RetractScore()
    {
        if (reference != "" && value != 0)
        {
            if (reference.Contains("_"))
            {
                foreach (var item in reference.Split('_'))
                {
                    if (scoreSystem.ContainsKey(item))
                    {
                        scoreSystem[item] -= value;
                    }
                }
            }
            else if (name.Contains("todos"))
            {
                scoreSystem["maria"] -= value;
                scoreSystem["ana"] -= value;
                scoreSystem["antonio"] -= value;
                scoreSystem["joao"] -= value;
                scoreSystem["pai"] -= value;
            }
            else if (scoreSystem.ContainsKey(reference))
            {
                scoreSystem[reference] -= value;
            }
            foreach (var person in scoreSystem.Keys)
            {
                Debug.Log("score: " + person + " - " + scoreSystem[person]);
            }
        }
    }

    public Dictionary<string, int> GetScoreSystem()
    {
        return scoreSystem;
    }

    public int GetScore(string reference)
    {
        if (scoreSystem.ContainsKey(reference))
        {
            return scoreSystem[reference];
        }
        return 0;
    }

    public void SetScoreSystem(Dictionary<string, int> savedScoreSystem)
    {
        scoreSystem = savedScoreSystem;
    }

    public void FinalAdvice()
    {
        if (scoreSystem["maria"] < 15)
        {
            advices += "    A Maria não gostou das decisões que tomaste perante ela. Tens de repensar as tuas ações.";
        }
        else if (scoreSystem["maria"] > 40)
        {
            advices += "    A Maria gostou das tuas decisões. Muito bem!";
        }
        else
        {
            advices += "    A Maria não gostou de algumas das tuas decisões. Podes melhorar as tuas decisões em algumas situações!";
        }
        if (scoreSystem["ana"] < 15)
        {
            advices += " A Ana não gostou das decisões que tomaste perante ela. Tens de repensar as tuas ações.";
        }
        else if (scoreSystem["ana"] > 40)
        {
            advices += " A Ana gostou das tuas decisões. Muito bem!";
        }
        else
        {
            advices += " A Ana não gostou de algumas das tuas decisões. Podes melhorar as tuas decisões em algumas situações!";
        }
        if (scoreSystem["antonio"] < 15)
        {
            advices += " O António não gostou das decisões que tomaste perante ele. Tens de repensar as tuas ações.";
        }
        else if (scoreSystem["antonio"] > 40)
        {
            advices += " O António gostou das tuas decisões. Muito bem!";
        }
        else
        {
            advices += " O António não gostou de algumas das tuas decisões. Podes melhorar as tuas decisões em algumas situações!";
        }
        if (scoreSystem["joao"] < 15)
        {
            advices += " O João não gostou das decisões que tomaste perante ele. Tens de repensar as tuas ações.\n";
        }
        else if (scoreSystem["joao"] > 40)
        {
            advices += " O João gostou das tuas decisões. Muito bem!\n";
        }
        else
        {
            advices += " O João não gostou de algumas das tuas decisões. Podes melhorar as tuas decisões em algumas situações!\n";
        }
        if (scoreSystem["pai"] < 15)
        {
            advices += "    Os teus pais não gostaram das atitudes e decisões que tomaste. Tens de repensar as tuas ações.";
        }
        else if (scoreSystem["pai"] > 45)
        {
            advices += "    Os teus pais ficaram satisfeitos ao ver as decisões que tomaste. Muito bem!";
        }
        else
        {
            advices += "    Os teus pais acharam que foste imprudente em alguns momentos. Podes melhorar as tuas decisões em algumas situações!";
        }
        if (scoreSystem["conta"] > 0)
        {
            advices += " Criaste conta com os teus amigos. Os teus pais ficaram desapontados. Ao criares conta numa nova rede social, deves sempre pedir-lhes autorização.\n";
        }
        else
        {
            advices += " Pediste permissão aos teus pais antes de criares conta numa nova rede social. Muito bem!\n";
        }
        if (scoreSystem["clara"] > 0)
        {
            advices += "    Aceitaste um pedido de amizade de uma desconhecida, a Clara. Tens de ter mais cuidado com os pedidos de amizade que aceitas.";
            if (scoreSystem["link"] > 0)
            {
                advices += " Para além disso, clicaste num link enviado por alguém que não conheces. Independentemente da situação, nunca o deves fazer.\n";
            }
            else
            {
                advices += " Apesar disso, não clicaste em nenhum link suspeito. Não te colocaste em riscos maiores!\n";
            }
        }
        else
        {
            advices += "    Recusaste o pedido de amizade de uma desconhecida, a Clara. Muito bem!\n";
        }
        if (scoreSystem["jaime"] > 0)
        {
            advices += "    Aceitaste um pedido de amizade de alguém que se fez passar pelo teu amigo Jaime, apesar de saberes que ele não tinha conta. Ele poderia ter criado conta desde a vossa conversa, mas deverias ter confirmado.";
            if (scoreSystem["jaimelink"] > 0)
            {
                advices += " Para além disso, clicaste num link enviado por esta pessoa. Independentemente da situação, nunca o deves fazer.\n";
            }
            else
            {
                advices += " Apesar disso, quando recebeste um link suspeito, decidiste bloquear e denunciar a conta. Conseguiste resolver a situação.\n";
            }
        }
        else
        {
            advices += "    Recusaste o pedido de amizade do Jaime. Sabias que ele não tinha conta. Muito bem!\n";
        }
        if (scoreSystem["password"] > 0)
        {
            advices += "    Partilhaste a tua password com os teus amigos. Nunca o deves fazer, mesmo que sejam pessoas de confiança.\n";
        }
        else
        {
            advices += "    Não partilhaste a tua password com ninguém. Muito bem!\n";
        }
        if (scoreSystem["post"] == -3)
        {
            advices += "    Fizeste um post imprudente que colocou em risco a tua segurança, ao partilhar algo ao pé de tua casa. Tens de ter cuidado com informações pessoais quando estás online.\n";
        }
        else if (scoreSystem["post"] == -1)
        {
            advices += "    Fizeste um post com algo que não era teu, o desenho da Maria. da próxima vez, certifica-te que tens autorização para partilhares.\n";
        }
        else
        {
            advices += "    O vídeo que partilhaste era seguro e não colocava a tua segurança em risco, nem era algo que era da autoria de outra pessoa. Muito bem!\n";
        }
        if (scoreSystem["password"] <= 2)
        {
            advices += "    Utilizaste uma palavra-passe fraca, contendo apenas letras. Deves utilizar sempre palavras-passe fortes, com uma combinação de letras maíusculas e minúsculas, números e símbolos.\n";
        }
        else if (scoreSystem["password"] == 3)
        {
            advices += "    Utilizaste uma palavra-passe média, contendo letras e números. Da próxima vez, podes fazer melhor. Tenta incluir também símbolos para a tornar mais segura.\n";
        }
        else
        {
            advices += "    A palavra-passe que utilizaste era forte, contendo uma combinação de letras maíusculas e minúsculas, números e símbolos. Muito bem!\n";
        }
        if (scoreSystem["wifi"] < 0)
        {
            advices += "    Estavas num sítio e decidiste ligar-te a uma rede Wi-Fi pública. Lembra-te que redes públicas podem ser inseguras. Se usares uma rede destas, nunca acedas a informações sensíveis ou pessoais.\n";
        }
        else
        {
            advices += "Utilizaste os dados móveis para aceder à internet. Uma opção muito mais segura do que o wifi público.\n";
        }

        if (scoreSystem["dicas1"] > 0)
        {
            advices += "    Deste um mau conselho à Ana. Deves evitar publicar fotos que divulguem informações, como a localização.";
        }
        else if (scoreSystem["dicas2"] > 0)
        {
            advices += "    Deste um bom conselho à Ana. A foto das árvores não divulgava a localização dela.";
        }

        if (scoreSystem["dicas3"] > 0)
        {
            advices += " Deste um mau conselho ao João. Deves evitar publicar fotos que divulguem informações, como a localização.";
        }
        else if (scoreSystem["dicas4"] > 0)
        {
            advices += " Deste um bom conselho ao João. O Facto daquelas flores serem as favoritas da mãe dele não mete ninguém em risco.";
        }

        if (scoreSystem["dicas5"] > 0)
        {
            advices += " Deves ter cuidado com quem partilhas fotos. Deves ter as tuas fotos apenas visíveis para quem é teu amigo.";
        }
        else if (scoreSystem["dicas6"] > 0)
        {
            advices += " Escolheste bem ao publicar a foto para que fosse visível apenas para os teus amigos.";
        }
        uiManager.ShowFinalAdvice(advices);
    }
}
