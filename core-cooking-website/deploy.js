const fs = require('fs-extra');

(async function build() {

	fs.emptyDir('../CoreCooking.Api/wwwroot', function (err) {
		if (err) {
			console.error(err);
		} else {
			console.log("Deleted the directory.");
		}
	}); //copies directory, even if it has subdirectories or files

	fs.copy('./dist/core-cooking-website', '../CoreCooking.Api/wwwroot', function (err) {
		if (err) {
			console.error(err);
		} else {
			console.log("Copied the files.");
		}
	}); //copies directory, even if it has subdirectories or files

	console.log("Deployment Done.");
})()