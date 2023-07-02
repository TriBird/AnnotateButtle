import pandas as pd
import pickle

# datasets = pd.DataFrame(data = [
#     [0, "今日のテニスの試合は緊張感溢れる展開だった！"],
#     [0, "バスケットボールの試合でダンクを決めた瞬間、最高の爽快感！"],
#     [0, "ゴルフのスイングを改善するために、コーチにアドバイスをもらった。"],
#     [0, "サッカーの試合でチームメイトと連携プレーを楽しんでいる。"],
#     [0, "今日は久しぶりにランニングをして、気分がリフレッシュした！"],
#     [0, "野球の試合でホームランボールをゲットし、一瞬の幸せを感じた。"],
#     [0, "バレーボールの試合で、劇的な逆転勝利を果たした！"],
#     [0, "スキージャンプの大会で、選手たちの勇敢な挑戦に感動した。"],
#     [0, "柔道の稽古で先生から新たな技を教えてもらった。"],
#     [0, "今日はスケートボードのトリックに挑戦して、大いに楽しんだ！"],
#     [1, "自家製ピザの具材を選ぶのが楽しい！"],
#     [1, "今日の夕食はヘルシーなサラダを作ってみた。"],
#     [1, "新鮮なイチゴを使ったスムージーは最高の朝食！"],
#     [1, "パスタのソースを手作りするのは時間がかかるけれど、やりがいがある。"],
#     [1, "お母さんのレシピで作るハンバーグはいつも美味しい。"],
#     [1, "クリーミーなチーズケーキを焼くのが得意な友達がいる。"],
#     [1, "今日はフレンチトーストを作って、幸せな朝をスタート！"],
#     [1, "カレーのスパイスを厳選して、本格的な味を楽しんだ。"],
#     [1, "みんなでピクニックに行って、BBQパーティーを開催！"],
#     [1, "魚介のパエリアを食べて、海の幸に舌鼓。"],
#     [2, "今日は山でハイキングをして、息をのむ景色を楽しんだ。"],
#     [2, "パリの街並みを歩いて、ロマンチックな雰囲気に包まれる。"],
#     [2, "空港での待ち時間に、お土産屋さんでお買い物を楽しんだ。"],
#     [2, "バリ島のビーチでゆったり過ごして、ストレス解消！"],
#     [2, "友達と一緒に温泉旅行に行って、心も体もリラックスした。 "],
#     [2, "パタヤビーチの青い海を眺めて、至福の時を過ごしました。 "],
#     [2, "日本の古都京都で和服を着て、昔ながらの風景に感動。 "],
#     [2, "ドバイの高層ビルに登って、息をのむ眺めに感激しました。 "],
#     [2, "ニューヨークの街を自転車で巡り、エネルギーをチャージ！ "],
#     [2, "アフリカのサファリで野生の動物たちに出会って、感動の一瞬。 "],
#     [3, "英語を勉強していると、新しい表現が増えて楽しい。 "],
#     [3, "フランス語の発音に苦戦しながらも、少しずつ上達している。"],
#     [3, "スペイン語の歌を聴いて、リズム感を感じる。"],
#     [3, "ドイツ語の文法は複雑だけど、学びがいがある！"],
#     [3, "中国語の漢字の書き順を覚えるのは大変だけれど、意外と面白い。"],
#     [3, "イタリア語のアクセントが難しくて、ネイティブと話すのは緊張する。"],
#     [3, "ロシア語の文字を読むのは難しいけれど、挑戦が楽しい。"],
#     [3, "韓国語の発音によるリズムをマスターするのは練習が必要だ。"],
#     [3, "アラビア語のアルファベットの独特な形が魅力的だ。"],
#     [3, "日本語の漢字を覚えると、より深い理解が得られる。"],
#     [4, "ピアノの音色に心が癒される。"],
#     [4, "ロックのライブコンサートの熱気が最高！"],
#     [4, "クラシック音楽を聴きながら、リラックスタイムを過ごす。"],
#     [4, "ジャズのリズムに合わせて、うっとりとしてしまう。"],
#     [4, "ヒップホップのビートに合わせて踊るのは楽しいエクササイズ。"],
#     [4, "アコースティックギターの音色が心に響く。 "],
#     [4, "エレクトロニックダンスミュージックで夜を盛り上げよう！"],
#     [4, "オペラの迫力ある歌声に、感動を覚えました。 "],
#     [4, "カントリーミュージックの歌詞に共感することが多い。 "],
#     [4, "ラテン音楽を聴いて、体を揺らしてノリノリ！"]
# ], columns = ["target", "text"])

datasets = pd.DataFrame(data=[
    [0, "The tennis match today was filled with tension!"],
    [0, "The moment I made a dunk in the basketball game, it was an exhilarating feeling!"],
    [0, "I received advice from a coach to improve my golf swing."],
    [0, "I'm enjoying playing in coordination with my teammates in the soccer match."],
    [0, "I went for a run today after a long time and refreshed my mood!"],
    [0, "I got a home run ball in the baseball game and felt a moment of happiness."],
    [0, "I achieved a dramatic comeback victory in the volleyball game!"],
    [0, "I was impressed by the brave challenges of the athletes in the ski jumping competition."],
    [0, "I learned a new technique from my teacher during judo practice."],
    [0, "Today, I challenged skateboard tricks and had a great time!"],
    [1, "It's fun to choose toppings for homemade pizza!"],
    [1, "I tried making a healthy salad for dinner today."],
    [1, "A smoothie made with fresh strawberries is the best breakfast!"],
    [1, "Making pasta sauce from scratch takes time, but it's rewarding."],
    [1, "My mom's recipe makes delicious hamburgers every time."],
    [1, "I have a friend who is good at baking creamy cheesecakes."],
    [1, "Today, I made French toast and started a happy morning!"],
    [1, "I enjoyed the authentic taste by carefully selecting spices for curry."],
    [1, "We went on a picnic together and held a BBQ party!"],
    [1, "I enjoyed seafood paella and savored the taste of the sea."],
    [2, "I went hiking in the mountains today and enjoyed breathtaking views."],
    [2, "Walking through the streets of Paris, surrounded by a romantic atmosphere."],
    [2, "I enjoyed shopping at a souvenir shop during the waiting time at the airport."],
    [2, "I relaxed and relieved stress while leisurely spending time on the beaches of Bali."],
    [2, "I went on a hot spring trip with friends and relaxed both mentally and physically."],
    [2, "I spent a blissful time looking at the blue sea of ​​Pattaya Beach."],
    [2, "I was moved by wearing a kimono in the ancient city of Kyoto, Japan, and seeing the traditional scenery."],
    [2, "I was impressed by the breathtaking view when climbing tall buildings in Dubai."],
    [2, "I rode a bicycle and toured the streets of New York, recharging my energy!"],
    [2, "I had a moment of excitement encountering wild animals on a safari in Africa."],
    [3, "Studying English is fun because I learn new expressions."],
    [3, "I'm gradually improving while struggling with the pronunciation of French."],
    [3, "Listening to Spanish songs, I can feel the rhythm."],
    [3, "German grammar is complex, but it's rewarding to learn!"],
    [3, "Remembering the stroke order of Chinese characters is difficult but surprisingly interesting."],
    [3, "The accent of Italian is difficult, and I feel nervous speaking with natives."],
    [3, "Reading Russian characters is difficult, but the challenge is enjoyable."],
    [3, "Mastering the rhythm based on the pronunciation of Korean is a matter of practice."],
    [3, "The unique shapes of Arabic alphabets are fascinating."],
    [3, "When I learn kanji in Japanese, I gain a deeper understanding."],
    [4, "The sound of the piano soothes my soul."],
    [4, "The energy of a live rock concert is amazing!"],
    [4, "I spend relaxing time listening to classical music."],
    [4, "I get absorbed in the rhythm of jazz."],
    [4, "Dancing to the beat of hip-hop is a fun exercise."],
    [4, "The sound of an acoustic guitar resonates in my heart."],
    [4, "Let's liven up the night with electronic dance music!"],
    [4, "I was impressed by the powerful singing in the opera."],
    [4, "I often relate to the lyrics of country music."],
    [4, "I sway my body and enjoy the Latin music!"]
], columns=["target", "text"])

datasets = datasets.reindex(columns=["text", "target"])


print(datasets.head(50))
with open('datasets_en.pkl', 'wb') as f:
    pickle.dump(datasets, f)









