set /A repoName=ActionableMessageHttpService
git init
git add .
git commit -m "Initial commit"
gh repo create %repoName% --source=. --public --push
pause