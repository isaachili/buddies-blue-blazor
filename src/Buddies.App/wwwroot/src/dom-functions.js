const interactionObservers = {};

function observeInteraction(id) {

	// Don't track the same element more than once
	if (!!interactionObservers[id]) {
		return;
	}

	const element = document.getElementById(id);

	if (!element) {
		return;
	}

	// Create interaction observer
	const observer = new IntersectionObserver(entries => {

		if (!entries[0].isIntersecting) {
			return;
		}

		$event = new Event("carouselpagescroll", {
			bubbles: true,
			target: entries[0].target,
		});

		// Dispatch event
		entries[0].target.dispatchEvent($event);
	}, {
		threshold: 1
	});

	// Observe element
	observer.observe(element);

	// Track observer for element
	interactionObservers[id] = observer;
}

function unobserveInteraction(id) {

	// Retrieve interaction observer
	observer = interactionObservers[id];

	if (!observer) {
		return;
	}

	// Retrieve element
	const element = document.getElementById(id);

	if (!element) {
		return;
	}

	// Unobserve element
	observer.unobserve(element);

	// Stop tracking observer
	delete interactionObservers[id];
}

function scrollIntoView(id) {

	const element = document.getElementById(id);

	if (!element) {
		return;
	}

	// Scroll element into view
	element.scrollIntoView({
		behavior: 'smooth',
	});
}
