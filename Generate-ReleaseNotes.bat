rem https://github.com/StefH/GitHubReleaseNotes

SET version=0.0.15

GitHubReleaseNotes --output ReleaseNotes.md --skip-empty-releases --exclude-labels question invalid doc --version %version%