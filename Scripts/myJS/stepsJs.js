$(document).ready(function () {
    var currentGfgStep, nextGfgStep, previousGfgStep;
    var opacity;
    var current = 1;
    var steps = $("fieldset").length;

    setProgressBar(current);

    
    $(".next-step").click(function () {

         if (valReq(current) == false) return false;

        currentGfgStep = $(this).parent();
        nextGfgStep = $(this).parent().next();

        $("#progressbar li").eq($("fieldset")
			.index(nextGfgStep)).addClass("active");

        nextGfgStep.show();
        currentGfgStep.animate({ opacity: 0 }, {
            step: function (now) {
                opacity = 1 - now;

                currentGfgStep.css({
                    'display': 'none',
                    'position': 'relative'
                });
                nextGfgStep.css({ 'opacity': opacity });
            },
            duration: 300
        });

        setProgressBar(++current);

    });


    $(".previous-step").click(function () {

        currentGfgStep = $(this).parent();
        previousGfgStep = $(this).parent().prev();

        $("#progressbar li").eq($("fieldset")
			.index(currentGfgStep)).removeClass("active");

        previousGfgStep.show();

        currentGfgStep.animate({ opacity: 0 }, {
            step: function (now) {
                opacity = 1 - now;

                currentGfgStep.css({
                    'display': 'none',
                    'position': 'relative'
                });
                previousGfgStep.css({ 'opacity': opacity });
            },
            duration: 300
        });
        setProgressBar(--current);
    });


    function setProgressBar(currentStep) {
        var percent = parseFloat(100 / steps) * current;
        percent = percent.toFixed();
        $(".progress-bar")
			.css("width", percent + "%")
    }

    $(".submit").click(function () {
        return false;
    })

    function valReq(currStep) {

        var bol, i;
        var matches = document.querySelectorAll(".req" + currStep.toString());

        bol = true;

        for (i = 0; i < matches.length; ++i) {

            console.log(matches[i].controltovalidate);
            if (matches[i].controltovalidate != null) {

                var ctrlById;

                ctrlById = document.getElementById(matches[i].controltovalidate);

                if (ctrlById.value == "") {
                    bol = false;
                    ctrlById.focus();
                    matches[i].style.display = "block";
                    //alert(ctrl1.innerText);
                    return false;
                }
                else {
                    matches[i].style.display = "none";
                    bol = true;
                }
            }

        }

        return bol;
    }

    //function valCP(currStep) {
    //    var bol, fieldId;

    //    fieldId = "field" + currStep.toString();

    //    //  console.log(fieldId);

    //    var ancestor = document.getElementById(fieldId)

    //    descendents = ancestor.children;

    //    var i, e, d, x, y;

    //    var ctrl, index, ctrlList;

    //    var ctrl1, index1, ctrllist1;

    //    bol = true;

    //    for (i = 0; i < descendents.length; ++i) {
    //        e = descendents[i];

    //        d = e.children

    //        // console.log(d);

    //        for (x = 0; x < d.length; ++x) {
    //            y = d[x];
    //            //console.log(y);

    //            ctrlList = y.children;

    //            for (index = 0; index < ctrlList.length; ++index) {
    //                ctrl = ctrlList[index];
    //                // console.log(ctrl);
    //                ctrlList1 = ctrl.children;


    //                for (index1 = 0; index1 < ctrlList1.length; ++index1) {
    //                    ctrl1 = ctrlList1[index1];

    //                    //console.log(ctrl1);
    //                    //console.log(ctrl1.controltovalidate);

    //                    if (ctrl1.controltovalidate != null) {
    //                        var ctrlById;

    //                        ctrlById = document.getElementById(ctrl1.controltovalidate);

    //                        if (ctrlById.value == "") {
    //                            bol = false;
    //                            ctrlById.focus();
    //                            ctrl1.style.display = "block";
    //                            //alert(ctrl1.innerText);
    //                            return false;
    //                        }
    //                        else {
    //                            ctrl1.style.display = "none";
    //                            bol = true;
    //                        }

    //                    }

    //                }

    //            }

    //        }
    //    }

    //    // console.log(bol);

    //    return bol;

    //}

});
