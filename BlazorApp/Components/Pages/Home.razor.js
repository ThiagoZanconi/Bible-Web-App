window.scrollToElement = (elementId) => {
    let element = document.getElementById("displayed_verse");
    if (element) {
        element.scrollIntoView({ behavior: "smooth", block: "start" });
    }
};

export function scrollToPrevButton(){
    let element = document.getElementById("prev_button");
    if (element) {
        element.scrollIntoView({ behavior: "smooth", block: "center" });
    }
}