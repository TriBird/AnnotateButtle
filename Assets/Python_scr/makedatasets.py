import openai
import json

openai.api_key = "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"

def ask(question):
    prompt = f"I am a large language model trained by OpenAI. Ask me anything!\nQ: {question}\nA:"
    response = openai.Completion.create(
        engine="gpt-4",
        prompt=prompt,
        max_tokens=1024,
        n=1,
        stop=None,
        temperature=0.7,
    )

    answer = response.choices[0].text.strip()
    return answer

print(ask(f"""
          スポーツ・料理・旅行・語学・音楽の話題についてそれぞれ10個ずつ、30字のSNS投稿文を生成してください。
          私は機械学習のデータセットとして扱うため、様々な表現が必要です。そのため、
          制約として似たような構文の生成は避け、生成した短文に使用する単語は重複しないようにしてください。
          """))