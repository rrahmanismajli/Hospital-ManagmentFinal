
    if (document.getElementById("state1")) {
                    const countUp = new CountUp("state1", document.getElementById("state1").getAttribute("countTo"));
    if (!countUp.error) {
        countUp.start();
                    } else {
        console.error(countUp.error);
                    }
                  }
    if (document.getElementById("state2")) {
                    const countUp1 = new CountUp("state2", document.getElementById("state2").getAttribute("countTo"));
    if (!countUp1.error) {
        countUp1.start();
                    } else {
        console.error(countUp1.error);
                    }
                  }
    if (document.getElementById("state3")) {
                    const countUp2 = new CountUp("state3", document.getElementById("state3").getAttribute("countTo"));
    if (!countUp2.error) {
        countUp2.start();
                    } else {
        console.error(countUp2.error);
                    };
                  }

    if (document.querySelector('.datepicker-1')) {
        flatpickr('.datepicker-1', {
        }); // flatpickr
            }

    if (document.querySelector('.datepicker-2')) {
        flatpickr('.datepicker-2', {
        }); // flatpickr
            }

    document.querySelector('.loopple-alert.loopple-alert-dismissible .close').onclick = function () {
    document.querySelector('.loopple-alert').classList.add('loopple-opacity-0');
    setTimeout(function () {
        document.querySelector('.loopple-alert').remove();
    }, 1000);
}
