export class TrainingBase {
    constructor(trainingId, isReverse) {
        this.isReverse = isReverse;
        this.trainingId = trainingId;
        if (!isReverse) {
            this.setImageBlur();
        }
        this.audio = new Audio();
        this.subscribeOnEvents();
    }

    newGame = function () {
        document.location.reload();
    }

    subscribeOnEvents() {
        this.subscribeOnNewGameEvent();
        this.subscribeOnPlayAudioEvent();
        this.subscribeOnGetNextAnswerEvent();
        this.subscribeOnSkipAnswerEvent();
    }



    get trainingArea() {
        return document.querySelector(".trainingArea");
    }

    subscribeOnNewGameEvent() {
        $(document).on("click", '.newGameBtn', _ => {
            document.location.reload();
        });
    }

    subscribeOnPlayAudioEvent() {
        let that = this;
        $(document).on('click', 'i.audio', function () {
            that.playAudio(this);
        });
    }


    subscribeOnGetNextAnswerEvent() {
        let that = this;
        $(document).on('click', '.trainingArea button.nextAnswerBtn ', function () {
            that.getNextAnswer(this);
        });
    }

    subscribeOnSkipAnswerEvent() {
        let that = this;
        $(document).on('click', '.trainingArea button.skipBtn ', function () {
            that.skipAnswer(this);
        });
    }

    checkAnswer(requestUrl, userAnswer) {
        $.ajax({
            url: requestUrl,
            type: 'GET',
            data: {
                trainingId: this.trainingId,
                answer: userAnswer
            },
            success: (res) => {
                document.querySelector("#answerRefreshArea").innerHTML = res;
                this.removeImageBlur();
            }
        });
    }

    getNextAnswer(button) {
        $.ajax({
            url: button.dataset.requestUrl,
            type: 'GET',
            data: {
                trainingId: this.trainingId
            },
            success: (res) => {
                this.trainingArea.innerHTML = res;
                if (!this.isReverse)
                   this.setImageBlur();
            }
        });
    }

    skipAnswer(button) {
        this.checkAnswer(button.dataset.requestUrl, null);
    }

    playAudio(button) {
        if (button.nodeName === "I") {
            button = button.parentElement;
        }
        let lang = this.isReverse ? 2 : 1;
        let audio = this.audio;
        $.ajax({
            url: button.dataset.requestUrl,
            type: 'GET',
            data: { word: button.previousElementSibling.innerText, language: lang },
            success: function (res) {
                audio.src = '';
                audio.src = res;
                audio.play();
            }
        });
    }

    setImageBlur() {
        let image = document.getElementById('wordImage');

        //показ результатов (картинки нет)
        if (image !== null) {
            image.style.filter = 'blur(8px)';
        }
    }

    removeImageBlur() {
        let image = document.getElementById('wordImage');
        if (image !== null) {
            image.style.filter = 'blur(0px)';
        }
    }
}