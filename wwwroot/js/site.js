// Общие небольшие улучшения интерфейса. Основная логика выполняется на сервере.
document.addEventListener("DOMContentLoaded", () => {
    document.querySelectorAll("[data-auto-focus]").forEach((element) => element.focus());
});
