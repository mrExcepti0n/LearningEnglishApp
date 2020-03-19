//(function () {

//    const dontKnowBtnClassName = "dontKnowBtn";

//    $(document).on("click", '.newGameBtn', _ => {
//        document.location.reload();

//    });


//    $(document).on('input', '.trainingArea input[type=\"text\"]', function () {
//        var button = document.querySelector("button.checkAnswerBtn");
//        if (this.value.length > 0) {
//            if (button.classList.contains(dontKnowBtnClassName)) {
//                button.classList.remove(dontKnowBtnClassName);
//                button.classList.remove('btn-warning');
//                button.classList.add('btn-primary');
//                button.innerText = "Проверить";
//            }
//        } else if (!button.classList.contains(dontKnowBtnClassName)) {
//            button.classList.add(dontKnowBtnClassName);

//            button.classList.remove('btn-primary');
//            button.classList.add('btn-warning');
//            button.innerText = "Не знаю";
//        }
//    });


//    $(document).on('click', '.trainingArea button.checkAnswerBtn ', function () {
//        checkAnswer(this);
//    });


//    $(document).on('click', '.trainingArea button.nextAnswerBtn ', function () {
//        getNextAnswer(this);
//    });


//    function checkAnswer(button) {
//        var input = document.querySelector(".trainingArea input[type=\"text\"]");
//        $.ajax({
//            url: button.dataset.requestUrl,
//            type: 'GET',
//            data: { answer: input.value },
//            success: (res) => {
//                document.querySelector(".trainingArea").innerHTML = res;
//            }
//        });
//    }

//    function getNextAnswer(button) {
//        $.ajax({
//            url: button.dataset.requestUrl,
//            type: 'GET',
//            success: (res) => {
//                document.querySelector(".trainingArea").innerHTML = res;
//            }
//        });
//    }
//})();


class TrainingBase {

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
        this.subscribeOnCheckAnswerEvent();
        this.subscribeOnGetNextAnswerEvent();
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

    subscribeOnCheckAnswerEvent() {
        let that = this;
        $(document).on('click', '.trainingArea button.answerBtn ', function () {
            that.checkAnswer(this);
        });
    }

    subscribeOnGetNextAnswerEvent() {
        let that = this;
        $(document).on('click', '.trainingArea button.nextAnswerBtn ', function () {
            that.getNextAnswer(this);
        });
    }

    checkAnswer(button) {
        $.ajax({
            url: button.dataset.requestUrl,
            type: 'GET',
            data: {
                trainingId: this.trainingId,
                answer: button.innerText
            },
            success: (res) => {
                document.querySelector(".answerArea").innerHTML = res;
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

    playAudio(button) {
        if (button.nodeName === "I") {
            button = button.parentElement;
        }
        let lang = this.isReverse ? 2 : 1;
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
        document.getElementById('wordImage').style.filter = 'blur(0px)';   
    }
}