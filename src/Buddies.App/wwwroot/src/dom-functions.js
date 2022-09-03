function scrollIntoView(id) {
	const element = document.getElementById(id);

	if (!element) {
		return;
	}

	element.scrollIntoView({
		behavior: 'smooth',
	});
}
