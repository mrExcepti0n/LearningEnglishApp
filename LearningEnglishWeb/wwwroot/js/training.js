(function () {

    const dontKnowBtnClassName = "dontKnowBtn";

    $(document).on("click", '.newGameBtn', _ => {
        document.location.reload();
    });


    $(document).on('input', '.trainingArea input[type=\"text\"]', function () {
        var button = document.querySelector("button.checkAnswerBtn");
        if (this.value.length > 0) {
            if (button.classList.contains(dontKnowBtnClassName)) {
                button.classList.remove(dontKnowBtnClassName);
                button.innerText = "Проверить";
            }
        } else if (!button.classList.contains(dontKnowBtnClassName)) {
            button.classList.add(dontKnowBtnClassName);
            button.innerText = "Не знаю";
        }
    });


    $(document).on('click', '.trainingArea button.checkAnswerBtn ', function () {
        checkAnswer(this);
    });


    $(document).on('click', '.trainingArea button.nextAnswerBtn ', function () {
        getNextAnswer(this);
    });


    function checkAnswer(button) {
        var input = document.querySelector(".trainingArea input[type=\"text\"]");
        $.ajax({
            url: button.dataset.requestUrl,
            type: 'GET',
            data: { answer: input.value },
            success: (res) => {
                document.querySelector(".trainingArea").innerHTML = res;
            }
        });
    }

    function getNextAnswer(button) {
        $.ajax({
            url: button.dataset.requestUrl,
            type: 'GET',
            success: (res) => {
                document.querySelector(".trainingArea").innerHTML = res;
            }
        });
    }
})();


class training {
    constructor(questions) {
        this.questions = questions;        
        this.currentQuestion = this.questions[0];
    }

    checkAnswer(userAnswer) {
        this.currentQuestion.checkAnswer(userAnswer);

        var currentQuestionIndex = this.questions.indexOf(this.currentQuestion);

        if (this.questions.length - 1 > currentQuestionIndex) {
            this.currentQuestion = this.questions[currentQuestionIndex + 1];
        }       
    }   
}

class question {
    constructor(title, answers, rightAnswer) {
        this.title = title;
        this.answers = answers;
        this.rightAnswer = rightAnswer;
        this.userAnswer = '';
    }    

    checkAnswer(userAnswer) {
        this.userAnswer = userAnswer;
        return userAnswer === this.rightAnswer;
    }
}