class training { 
    constructor(checkAnswerUrl, getNextQuestionUrl) {
        this.checkAnswerUrl = checkAnswerUrl;
        this.getNextQuestionUrl = getNextQuestionUrl;
    }

    subscribeOnEvents = function() {
        $(document).on("click", '.newGameBtn', _ => {
            document.location.reload();
        });

        var checkUrl = this.checkAnswerUrl;
        var getNextUrl = this.getNextQuestionUrl;

        $(document).on('input', '.trainingArea input[type=\"text\"]', function () {
            var button = document.querySelector("button.checkAnswer");
            if (this.value.length > 0) {
                if (button.classList.contains("dontKnow")) {
                    button.classList.remove("dontKnow");
                    button.innerText = "Проверить";
                }
            } else if (!button.classList.contains("dontKnow")) {
                button.classList.add("dontKnow");
                button.innerText = "Не знаю";
            }
        });

        $(document).on('click', '.trainingArea button.train-btn', function () {
            if (this.classList.contains("checkAnswer")) {
                checkAnswer(this);
            } else {
                getNextAnswer(this);
            }
        });

        function checkAnswer() {
            var input = document.querySelector(".trainingArea input[type=\"text\"]");
            $.ajax({
                url: checkUrl ,
                type: 'GET',
                data: { answer: input.value },
                success: (res) => {
                    document.querySelector(".trainingArea").innerHTML = res;
                }
            });
        }

        function getNextAnswer() {
            $.ajax({
                url: getNextUrl,
                type: 'GET',
                success: (res) => {
                    document.querySelector(".trainingArea").innerHTML = res;
                }
            });
        }
    }
}




(function () {

  
})();